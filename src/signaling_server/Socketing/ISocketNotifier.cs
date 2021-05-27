
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace signaling_server.Socketing
{
    public interface ISocketNotifier
    {
        Task SendServerAvailable(SocketData socketData, string serverOffer);
        Task SendAnswerToServer(SocketData serverData, string clientAnswer);
        Task SendMessageAsync(WebSocket socket, object response);
    }
}
