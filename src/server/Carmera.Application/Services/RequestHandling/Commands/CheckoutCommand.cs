using System.Net;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckOutCommand : CommandBase
    {
        public CheckOutCommand(string peerName, IPAddress address, int port) : base(peerName, address, port)
        { }
    }
}