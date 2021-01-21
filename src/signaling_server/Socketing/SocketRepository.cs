using System;
using System.Collections.Generic;
using System.Linq;

namespace signaling_server.Socketing
{
    public class SocketRepository : ISocketRepository
    {
        private List<SocketData> _sockets;

        public SocketRepository()
        {
            _sockets = new List<SocketData>();
        }

        public bool ContainsServer() => _sockets.Any(s => s.SocketKind == SocketKind.server);

        public IEnumerable<SocketData> GetAllClients() => _sockets.Where(s => s.SocketKind == SocketKind.client);

        public SocketData GetServer() => _sockets.FirstOrDefault(s => s.SocketKind == SocketKind.server);

        public SocketData GetSocket(Guid id) => _sockets.FirstOrDefault(s => s.Id == id);

        public void OnSocketConnected(SocketData data)
        {
            if (!_sockets.Any(s => s.Address == data.Address) && !_sockets.Any(s => s.SocketKind == data.SocketKind))
            {
                _sockets.Add(data);
            }
        }

        public void OnSocketDisconnected(Guid id)
        {
            var found = _sockets.FirstOrDefault(s => s.Id == id);
            if (found != null)
            {
                _sockets.Remove(found);
            }
        }
    }
}
