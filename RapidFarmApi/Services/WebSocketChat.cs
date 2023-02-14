using System.Net.WebSockets;
using System.Text;
using RapidFarmApi.Abstractions;

namespace RapidFarmApi.Services
{
    public class WebSocketChat : WebSocketHandler
    {
        public WebSocketChat(SocketManager socketManagerService)
            : base(socketManagerService) {}

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
        }
        public override async Task RecieveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string messageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
            await SendRecieversMessage(socket, messageJson);
        }
    }
}