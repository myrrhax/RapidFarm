using System.Net.WebSockets;
using RapidFarmApi.Abstractions;

namespace RapidFarmApi.Middlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.WebSockets.IsWebSocketRequest || httpContext.User == null)
                return;

            var socket = await httpContext.WebSockets.AcceptWebSocketAsync();
            await _webSocketHandler.OnConnected(socket);

            await Recieve(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        await _webSocketHandler.RecieveAsync(socket, result, buffer);
                        return;
                    }
                    else if(result.MessageType == WebSocketMessageType.Close) 
                    {
                        await _webSocketHandler.OnDisconnected(socket);
                        return;
                    }
                });
        }

        private async Task Recieve(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}