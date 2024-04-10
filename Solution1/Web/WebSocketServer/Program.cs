using System.Net;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using Fleck;
using PizzaStore.Models;

namespace WebsocketServer
{
    class Program
    {
        static void Main() // PEMPASS
        {
            List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
            WebSocketServer server = new WebSocketServer("wss://127.0.0.1:8143");
            WebSocketHandler handler = new WebSocketHandler();
            server.Certificate = new X509Certificate2("Data/Security/certificate.pfx");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = async message =>
                {
                    Console.WriteLine(message);
                    string? received = await handler.ReceiveMessage(new Guid(), message);
                    if (received != null) allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };
            });
            
            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }
        }
    }
}