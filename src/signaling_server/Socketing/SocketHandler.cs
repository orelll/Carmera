using Newtonsoft.Json;
using signaling_server.MessageProcessing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace signaling_server.Socketing
{
    public class SocketHandler
    {
        private readonly IRequestProcessor _requestProcessor;
        private readonly ISocketNotifier _notifier;


        public SocketHandler(IRequestProcessor requestProcessor, ISocketNotifier notifier)
        {
            _requestProcessor = requestProcessor;
            _notifier = notifier;
        }

        public async Task ReceiveSocket(WebSocket socket, IPAddress address)
        {

            var buffer = new byte[1024 * 4];

            var wholeList = new List<byte>();

            while (socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = null;

                do
                {
                    result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                            cancellationToken: CancellationToken.None);

                    wholeList.AddRange(buffer);
                    buffer = new byte[1024 * 4];
                }
                while (!result.EndOfMessage);



                await HandleSocket(socket, result, wholeList.ToArray(), address);
            }
        }

        private async Task HandleSocket(WebSocket socket, WebSocketReceiveResult result, byte[] buffer, IPAddress address)
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var response = _requestProcessor.ProcessRequest(buffer, address, socket);
                var serialized = JsonConvert.SerializeObject(response);
                await _notifier.SendMessageAsync(socket, serialized);

                return;
            }

            else if (result.MessageType == WebSocketMessageType.Close)
            {
                return;
            }
        }

    }
}
