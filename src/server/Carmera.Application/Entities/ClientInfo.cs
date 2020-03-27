using System.Net;

namespace Carmera.Application.Entities
{
    public class ClientInfo
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
    }
}