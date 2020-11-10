using System.Net;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckInCommand : CommandBase
    {
        public string Offer { get; set; }

        public CheckInCommand(string offer, string peerName, IPAddress address, int port) : base(peerName, address, port)
        {
            Offer = offer;
        }
    }
}