using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
using PizzaStore.Models.Security;

namespace PizzaStore.Models;

public class WebSocketHandler : IWebSocketHandler
{
    public List<SocketConnection> websocketConnections = new List<SocketConnection>();
    private Order? CurrentOrder = null;

    public async Task Handle(Guid id, WebSocket webSocket)
    {
        lock (websocketConnections) { 
            websocketConnections.Add(new SocketConnection { 
                Id = id,
                WebSocket = webSocket
            });
        }

        await SendMessageToSockets($"{{ \"id\": \"{id}\" }}");

        while (webSocket.State == WebSocketState.Open)
        {
            string? message = await ReceiveMessage(id, webSocket);
            if (message != null) await SendMessageToSockets(message);
        }
    }

    private async Task<string> ReceiveMessage(Guid id, WebSocket webSocket)
    {
        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[8192]);
        WebSocketReceiveResult response = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
        string jsonString = Encoding.ASCII.GetString(buffer);
        if (CurrentOrder == null) {
            // necessary because of the singleton architecture
            Customer.Instance.FromJson(jsonString);
            CurrentOrder = new Order();
        } else {
            Order tempOrder = MessageInspector.ParseJson<Order>(jsonString);
            CurrentOrder.AddPizzas(tempOrder.Pizzas);
        }
        return JsonConvert.SerializeObject(CurrentOrder);
    }

    private async Task SendMessageToSockets(string message)
    {
        IEnumerable<SocketConnection> toSendTo;

        lock (websocketConnections)
        {
            toSendTo = websocketConnections.ToList();
        }

        IEnumerable<Task> tasks = toSendTo.Select(async websocketConnection =>
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            await websocketConnection.WebSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        });
        
        await Task.WhenAll(tasks);
    }
}