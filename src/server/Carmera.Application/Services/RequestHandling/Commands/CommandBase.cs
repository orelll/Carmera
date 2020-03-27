using System.Net;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public abstract class CommandBase<TReq> : ICommand<TReq>
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