using System.Net.WebSockets;

namespace PizzaStore.Models;

public class SocketConnection
{
    public Guid Id { get; set; }
    public WebSocket WebSocket { get; set; }
}