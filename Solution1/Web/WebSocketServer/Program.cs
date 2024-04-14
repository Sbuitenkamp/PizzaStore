using System.Security.Cryptography.X509Certificates;
using Fleck;
using PizzaStore.Models;
using System.Security.Authentication;
using Newtonsoft.Json;

namespace WebsocketServer
{
    class Program
    {
        static void Main()
        {
            Dictionary<Guid, Order?> CurrentOrders = new Dictionary<Guid, Order?>();
            Dictionary<Guid, IWebSocketConnection> allSockets = new Dictionary<Guid, IWebSocketConnection>();
            WebSocketServer server = new WebSocketServer("wss://0.0.0.0:8181")
            {
                EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
            };
            string certPem = File.ReadAllText("Data/Security/certificate.crt");
            string keyPem = File.ReadAllText("Data/Security/private.key");
            server.Certificate = X509Certificate2.CreateFromPem(certPem, keyPem);
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Guid guid = Guid.NewGuid();
                    Console.WriteLine("Opened connection to: " + guid.ToString());
                    allSockets.Add(guid, socket);
                };
                socket.OnClose = () =>
                {
                    Guid guid = allSockets.First(x => x.Value == socket).Key;
                    Console.WriteLine("Closed connection with: " + guid.ToString());
                    allSockets.Remove(guid);
                };
                socket.OnMessage = async message =>
                {
                    Guid guid = allSockets.First(x => x.Value == socket).Key;
                    Order? order;

                    try { // prevent not recognised key exception
                        order = CurrentOrders[guid];
                    } catch {
                        order = null;
                    }
                    
                    Order? receivedOrder = WebSocketHandler.Instance.ReceiveMessage(message, order);
                    if (order == null && receivedOrder != null) CurrentOrders.Add(guid, receivedOrder);
                    string msg = JsonConvert.SerializeObject(receivedOrder);
                    if (receivedOrder != null) await socket.Send(msg);
                };
            });
            
            // prevent shut down
            var input = Console.ReadLine();
            while (input != "exit")
            {
                input = Console.ReadLine();
            }
        }
    }
}