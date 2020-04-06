using Carmera.Application.Services.RequestHandling.Contracts;
using System.Net;

namespace Carmera.Application.Services.RequestHandling
{
    public class TestCommand<TOut> : Request where TOut : Result
    {
        public string PeerName { get; }
        public IPAddress Address { get; }
        public int Port { get; }
    }
}