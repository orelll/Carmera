using System.Net;

namespace Carmera.WebHost
{
    public class ClientInfo
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
    }
}