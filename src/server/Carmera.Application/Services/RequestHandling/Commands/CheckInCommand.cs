using System.Net;
using Carmera.Application.Services.RequestHandling.Commands.Results;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckInCommand : CommandBase<CheckInCommandResult>
    {
        public CheckInCommand(string peerName, IPAddress address, int port) : base(peerName, address, port)
        { }
    }
}