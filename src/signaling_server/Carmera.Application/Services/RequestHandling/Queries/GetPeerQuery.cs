using System.Net;

namespace Carmera.Application.Services.RequestHandling.Queries
{
    public class GetPeerQuery : QueryBase
    {
        public string SecondPeerName { get; }

        public GetPeerQuery(string peerName, IPAddress address, int port, string secondPeerName) : base(peerName, address, port)
        {
            SecondPeerName = secondPeerName;
        }
    }
}