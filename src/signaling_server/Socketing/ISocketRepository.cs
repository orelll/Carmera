using System;
using System.Collections.Generic;

namespace signaling_server.Socketing
{
    public interface ISocketRepository
    {
        void OnSocketConnected(SocketData data);
        void OnSocketDisconnected(Guid id);
        bool ContainsServer();
        SocketData GetServer();
        IEnumerable<SocketData> GetAllClients();
        SocketData GetSocket(Guid id);
    }
}
