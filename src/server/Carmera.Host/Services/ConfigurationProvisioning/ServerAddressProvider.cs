using System;
using Carmera.Host.Services.ConfigurationProvisioning.DTO;

namespace Carmera.Host.Services.ConfigurationProvisioning
{
    public class ServerAddressProvider : IConfigurationProvider<ServerConfiguration>
    {
        public ServerConfiguration GetConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}