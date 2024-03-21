using System.Net.WebSockets;

namespace PizzaStore.Models;

public interface IWebSocketHandler
{
    Task Handle(Guid id, WebSocket websocket);
}