using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
using PizzaStore.Models.Security;
using Fleck;

namespace PizzaStore.Models;

public class WebSocketHandler
{
    public List<SocketConnection> websocketConnections = new List<SocketConnection>();
    private Order? CurrentOrder = null;

    public Task<string> ReceiveMessage(Guid id, string message)
    {
        // string jsonString = Encryptor.DecryptString(jsonStringEncrypted);
        
        if (CurrentOrder == null) {
            // necessary because of the singleton architecture
            Customer.Instance.FromJson(message);
            CurrentOrder = new Order();
        } else {
            Order tempOrder = MessageInspector.ParseJson<Order>(message);
            CurrentOrder.AddPizzas(tempOrder.Pizzas);
        }
        return Task.FromResult(JsonConvert.SerializeObject(CurrentOrder));
    }
}