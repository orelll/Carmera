using System;
using Carmera.Host.Services.ConfigurationProvisioning;
using Carmera.Host.Services.Logging;

namespace Carmera.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var logger = new ConsoleLogger();
            var configProvider = new DummyServerAddressProvider();
            var server = new WebSocketServer(configProvider, logger);

            try
            {
                server.Start();
            }
            catch (Exception a)
            {
              
            }

            Console.WriteLine("Something is no yes xD");
            Console.ReadKey();
        }
    }
}
