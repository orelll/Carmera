using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Carmera.Host.Services.ConfigurationProvisioning.DTO;
using Carmera.Host.Services.Logging;

namespace Carmera.Host
{
    public class WebSocketServer
    {
        private readonly ServerConfiguration _configuration;
        private readonly ILogger _logger;
        private TcpListener _server;

        public WebSocketServer(IConfigurationProvider<ServerConfiguration> configProvider, ILogger logger)
        {
            _configuration = configProvider.GetConfiguration() ?? throw new ArgumentNullException(nameof(_configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Start()
        {
            _server = new TcpListener(_configuration.Address, _configuration.Port);
            _server.Start();
            _logger.Log($"Server startied under {_configuration.Address}:{_configuration.Port}");

            while (true)
            {
                _logger.Log("Waiting for connections...");

                var client = _server.AcceptTcpClient();
                new Task(() => HandleClient(client)).Start();
            }
        }

        private async void HandleClient(TcpClient client)
        {
            _logger.Log($"Client {client.Client.RemoteEndPoint.ToString()} connected");

            //TODO: how to break this?
            using (var stream = client.GetStream())
                while (true)
                {
                    while (!stream.DataAvailable) ;
                    while (client.Available > 3)
                    {
                        var bytes = new byte[client.Available];
                        await stream.ReadAsync(bytes, 0, client.Available);

                        var message = Encoding.UTF8.GetString(bytes);
                        _logger.Log($"Received message '{message}'");
                    }
                }

            _logger.Log("Client is gone");
        }
    }
}