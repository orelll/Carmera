using Newtonsoft.Json;
using signaling_server.MessageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace signaling_server.Socketing
{
    public class SocketHandler
    {
        private readonly IRequestProcessor _requestProcessor;
        

        public SocketHandler(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
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
                var response =  _requestProcessor.ProcessRequest(buffer, address); 
                var serialized =  JsonConvert.SerializeObject(response);
                await SendMessageAsync(socket, serialized);
                await socket.CloseAsync( WebSocketCloseStatus.NormalClosure, "processed", CancellationToken.None);
                return;
            }

            else if (result.MessageType == WebSocketMessageType.Close)
            {
                return;
            }
        }

        private async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                    offset: 0,
                                                                    count: message.Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true,
                                    cancellationToken: CancellationToken.None);
        }
    }
}
