using System.Net;

namespace Carmera.Application.Services.RequestHandling.Queries
{
    public class QueryBase : Query
    {
        public string PeerName { get; }
        public IPAddress Address { get; }
        public int Port { get; }

        public QueryBase(string peerName, IPAddress address, int port)
        {
            PeerName = peerName;
            Address = address;
            Port = port;
        }
    }
}