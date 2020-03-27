using System.Net;
using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckOutCommand : CommandBase<CheckOutRequestDTO>
    {
        public CheckOutCommand(string peerName, IPAddress address, int port) : base(peerName, address, port)
        { }
    }
}