using System.Net;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckInCommand : CommandBase
    {
        public CheckInCommand(string peerName, IPAddress address, int port) : base(peerName, address, port)
        { }
    }
}