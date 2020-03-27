using System.Net;
using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckInCommand : CommandBase<CheckInRequestDTO>
    {
        public CheckInCommand(string peerName, IPAddress address, int port) : base(peerName, address, port)
        { }
    }
}