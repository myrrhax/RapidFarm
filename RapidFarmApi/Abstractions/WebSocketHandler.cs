using System.Net.WebSockets;
using System.Text;
using RapidFarmApi.Services;

namespace RapidFarmApi.Abstractions
{
    public abstract class WebSocketHandler
    {
        protected SocketManager SocketManager { get; set; }

        public WebSocketHandler(SocketManager socketManager) 
        {
            SocketManager = socketManager;
        }
        public virtual async Task OnConnected(WebSocket socket)
        {
            SocketManager.AddSocket(socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await SocketManager.RemoveSocket(SocketManager.GetSocketId(socket));
        }

        public async Task SendRecieversMessage(WebSocket socket, string msg)
        {
            string socketId = SocketManager.GetSocketId(socket);
            List<string> recievers = SocketManager.GetRecievers(socketId);

            foreach (string reciever in recievers) 
            {
                WebSocket recieverSocket = SocketManager.GetSocketById(reciever);
                await SendMessageAsync(recieverSocket, msg);
            }
        }

        private async Task SendMessageAsync(WebSocket socket, string msg)
        {
            if (socket.State != WebSocketState.Open)
                return;

            var buffer = new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(msg),
                                                offset: 0,
                                                count: msg.Length);
            await socket.SendAsync(buffer: buffer, 
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
        }

        public abstract Task RecieveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}