using System.Net;
using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Application.Services.RequestHandling.Query.Results;

namespace Carmera.Application.Services.RequestHandling.Query
{
    public class GetPeerQuery : CommandBase<GetPeerQueryResult>
    {
        public string SecondPeerName { get; }

        public GetPeerQuery(string peerName, IPAddress address, int port, string secondPeerName) : base(peerName, address, port)
        {
            SecondPeerName = secondPeerName;
        }
    }
}