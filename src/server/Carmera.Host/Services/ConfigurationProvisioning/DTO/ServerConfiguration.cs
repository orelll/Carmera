using System.Net;

namespace Carmera.Host.Services.ConfigurationProvisioning.DTO
{
    public class ServerConfiguration
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
    }
}