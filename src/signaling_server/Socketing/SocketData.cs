using System;
using System.Net;

namespace signaling_server.Socketing
{
    public class SocketData
    {
        public Guid Id { get; }
        public SocketKind SocketKind { get; set; }
        public IPAddress Address { get; set; }
        public string Offer { get; set; }

        public SocketData() => Id = Guid.NewGuid();
    }
}
