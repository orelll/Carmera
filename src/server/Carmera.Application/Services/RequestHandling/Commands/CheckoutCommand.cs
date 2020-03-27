using System.Net;
using Carmera.Application.Services.RequestHandling.Commands.Results;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckOutCommand : CommandBase<CheckOutCommandResult>
    {
        public CheckOutCommand(string peerName, IPAddress address, int port) : base(peerName, address, port)
        { }
    }
}