using System.Net;
using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class GetPeerCommand : CommandBase<GetPeerRequestDTO>
    {
        public string SecondPeerName { get; }

        public GetPeerCommand(string peerName, IPAddress address, int port, string secondPeerName) : base(peerName, address, port)
        {
            SecondPeerName = secondPeerName;
        }
    }
}