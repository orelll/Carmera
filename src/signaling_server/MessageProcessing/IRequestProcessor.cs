
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace signaling_server.MessageProcessing
{
    public interface IRequestProcessor
    {
        object ProcessRequest(byte[] requestBytes, IPAddress address, WebSocket socket);
    }
}
