using System.Net;

namespace Carmera.WebHost
{
    public class PeerInfo
    {
        public string Payload { get; set; }
        public IPAddress Address { get; set; }
        public int Port { get; set; }

        public PeerInfo(string payload, IPAddress address, int port) 
        {
            Payload = payload;
            Address = address;
            Port = port;
        }
    }
}