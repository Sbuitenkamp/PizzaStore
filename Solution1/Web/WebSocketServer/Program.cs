using System.Net;
using System.Net.WebSockets;
using PizzaStore.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();
app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest) {
        using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
        WebSocketHandler handler = new WebSocketHandler();
        await handler.Handle(new Guid(), webSocket);
    } else {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

app.Run("http://localhost:5050");
