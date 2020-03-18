using System.Net;
using Carmera.Host.Services.ConfigurationProvisioning.DTO;

namespace Carmera.Host.Services.ConfigurationProvisioning
{
    public class DummyServerAddressProvider : IConfigurationProvider<ServerConfiguration>
    {
        public ServerConfiguration GetConfiguration() => new ServerConfiguration { Address = IPAddress.Parse("127.0.0.1"), Port = 8080 };
    }
}