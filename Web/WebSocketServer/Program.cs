using System.Net;
using System.Net.WebSockets;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest) {
        using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

        byte[] data = Encoding.ASCII.GetBytes($"aaaaaaaaaaa");
        await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "random closing", CancellationToken.None);
    } else {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

app.Run("http://localhost:5050");
