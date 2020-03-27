using System.Net;
using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public abstract class CommandBase<TResp> : ICommand<TResp> where TResp : IResult
    {
        public string PeerName { get; }
        public IPAddress Address { get; }
        public int Port { get; }

        public CommandBase(string peerName, IPAddress address, int port)
        {
            PeerName = peerName;
            Address = address;
            Port = port;
        }
    }
}