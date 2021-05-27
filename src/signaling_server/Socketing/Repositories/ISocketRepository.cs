using System;
using System.Collections.Generic;

namespace signaling_server.Socketing.Repositories
{
    public interface ISocketRepository
    {
        void OnSocketConnected(SocketData data);
        void OnSocketDisconnected(Guid id);
        bool ContainsServer();
        SocketData GetServer();

        IEnumerable<SocketData> GetAllServers();
        void ClearAllServers();

        IEnumerable<SocketData> GetAllRegistrations();
        void ClearAllRegistrations();

        IEnumerable<SocketData> GetAllClients();
        void ClearAllClients();

        SocketData GetSocket(Guid id);
    }
}
