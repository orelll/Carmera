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
            var handShaken = false;

            //TODO: how to break this?
            using (var stream = client.GetStream())
            {
                while (true)
                {
                    while (!stream.DataAvailable) ;
                    while (client.Available > 3)
                    {
                        if (!handShaken)
                        {
                            await DoHandshake(client, stream);
                            handShaken = true;
                        }
                        else{
                            var message = await GetClientMessage(client, stream);
                            _logger.Log($"New client message, yay! {Environment.NewLine}{message}");

                            var response = Encoding.UTF8.GetBytes("Hello there!");
                            stream.Write(response, 0, response.Length);
                        }
                    }
                }
            }
        }

        private async Task<string> GetClientMessage(TcpClient client, NetworkStream stream)
        {
            var bytes = new byte[client.Available];
            await stream.ReadAsync(bytes, 0, client.Available);
            var x = String.Join(' ', bytes);
            var message = Encoding.ASCII.GetString(bytes);
            _logger.Log($"Received message '{message}'");
            return message;
        }

        private async Task DoHandshake(TcpClient client, NetworkStream stream)
        {
            _logger.Log($"Handshaking {client}...");
            var message = await GetClientMessage(client, stream);

            var response = PrepareResponse(message);
            stream.Write(response, 0, response.Length);
            _logger.Log("Handskake done!");
        }

        private byte[] PrepareResponse(string data)
        {
            const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker

            var messageToSend = "HTTP/1.1 101 Switching Protocols" + eol
                + "Connection: Upgrade" + eol
                + "Upgrade: websocket" + eol
                + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                    System.Security.Cryptography.SHA1.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(
                            new System.Text.RegularExpressions.Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                        )
                    )
                ) + eol
                //+ "Sec-WebSocket-Protocol: echo-protocol" + eol
                + eol;

            var response = Encoding.UTF8.GetBytes(messageToSend);

            return response;
        }
    }
}