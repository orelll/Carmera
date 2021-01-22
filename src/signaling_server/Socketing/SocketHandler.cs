using Newtonsoft.Json;
using signaling_server.MessageProcessing;
using System;
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

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                await HandleSocket(socket, result, buffer, address);
            }
        }

        private async Task HandleSocket(WebSocket socket, WebSocketReceiveResult result, byte[] buffer, IPAddress address)
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var response =  _requestProcessor.ProcessRequest(buffer, address, socket); 
                var serialized =  JsonConvert.SerializeObject(response);
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
