using System;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Socketing
{
    public class SocketData
    {
        public Guid Id { get; }
        public SocketKind SocketKind { get; set; }
        public IPAddress Address { get; set; }
        public string Offer { get; set; }
        public WebSocket Socket { get; set; }
        public bool Notified { get; set; } = false;

        public SocketData(WebSocket socket, SocketKind kind, IPAddress address, string offer)
        {
            Id = Guid.NewGuid();
            SocketKind = kind;
            Address = address;
            Offer = offer;
            Socket = socket;
        }
    }
}
