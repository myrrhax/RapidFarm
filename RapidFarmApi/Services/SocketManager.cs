using System.Collections.Concurrent;
using System.Net.WebSockets;
using RapidFarmApi.Exceptions;

namespace RapidFarmApi.Services
{
    public class SocketManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new();
        public WebSocket GetSocketById(string socketId)
        {
            return _sockets.FirstOrDefault(s => s.Key== socketId).Value;
        }

        public string GetSocketId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(s => s.Value == socket).Key;
        }

        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(CreateConnectionId(), socket);
        }

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public List<string> GetRecievers(string senderId) 
        {
            List<string> recievers = _sockets.Keys
                .Where(socketId => socketId != senderId).ToList();
            return recievers;
        }

        public async Task RemoveSocket(string socketId)
        {
            bool isRemoved = _sockets.TryRemove(socketId, out WebSocket? socket);

            if (!isRemoved)
                throw new SocketDoesntExists(socketId);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by Connection Manager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}