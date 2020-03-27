using System.Net;

namespace Carmera.Application.Services.RequestHandling.Contracts
{
    public interface IRequest<out TResp> where TResp: IResult
    {
        string PeerName { get; }
        IPAddress Address { get; }
        int Port { get; }
    }
}