using Fleck;

namespace PizzaStore.Models;

public interface IWebSocketHandler
{
    Task Handle(Guid id, string websocket);
}