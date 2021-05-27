using Newtonsoft.Json;
using signaling_server.Socketing.Notifications;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace signaling_server.Socketing
{
    public class SocketNotifier : ISocketNotifier
    {
        public async Task SendServerAvailable(SocketData socketData, string serverOffer)
        {
            var response = new ServerAvailableNotification(serverOffer);
            await SendMessageAsync(socketData.Socket, response);
        }

        public async Task SendMessageAsync(WebSocket socket, object response)
        {
            if (socket.State != WebSocketState.Open)
                return;

            var message = JsonConvert.SerializeObject(response);
            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                    offset: 0,
                                                                    count: message.Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true,
                                    cancellationToken: CancellationToken.None);
        }

        public async Task SendAnswerToServer(SocketData serverData, string clientAnswer)
        {
            var notification = new PeerAnswerNotification(clientAnswer);
            await SendMessageAsync(serverData.Socket, notification);
        }
    }
}
