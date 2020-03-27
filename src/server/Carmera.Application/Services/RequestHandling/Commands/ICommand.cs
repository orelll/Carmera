using System.Net;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public interface ICommand<TReq> : IRequest
    {
        string PeerName { get;  }
        IPAddress Address { get;  }
        int Port { get; }
    }
}