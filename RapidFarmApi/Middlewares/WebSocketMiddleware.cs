using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Entities;
using RapidFarmApi.Models;

namespace RapidFarmApi.Middlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler;
        private readonly IStateRepository _stateRepo;
        private readonly IScriptsRepository _scriptRepo;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler, IStateRepository stateRepo, IScriptsRepository scriptRepo)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
            _stateRepo = stateRepo;
            _scriptRepo = scriptRepo;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.WebSockets.IsWebSocketRequest || httpContext.User == null)
                return;

            var socket = await httpContext.WebSockets.AcceptWebSocketAsync();
            
            await _webSocketHandler.OnConnected(socket);

            State? currentState = await _stateRepo.GetLastState();
            PlantScript currentScript = await _scriptRepo.GetCurrentScriptAsync();
            SocketMessage socketMessage = new SocketMessage() { CurrentScript = currentScript, State = currentState };
            
            if (currentState == null) 
            {
                socketMessage.Errors.Add("Arduino client is offline");
            }
            await _webSocketHandler.SendMessageAsync(socket, JsonSerializer.Serialize<SocketMessage>(socketMessage));

            await Recieve(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string msgJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        SocketMessage? message = JsonSerializer.Deserialize<SocketMessage>(msgJson);
                        if (message?.State != null) 
                        {
                            await _stateRepo.AddState(message.State);                                
                        }
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