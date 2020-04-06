using System.Net;

namespace Carmera.Application.Services.RequestHandling.Contracts
{
    public class Request
    {
        string PeerName { get; }
        IPAddress Address { get; }
        int Port { get; }
    }
}