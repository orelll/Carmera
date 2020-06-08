using System.Net;

namespace Carmera.Common.Common
{
    public class ClientAddress
    {
        public IPAddress Address { get; }
        public int Port { get; }

        public ClientAddress(IPAddress address, int port)
        {
            // shouldn't it be validated?
            Address = address;
            Port = port;
        }
    }
}