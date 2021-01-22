using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public class GetServerRequest:RequestBase<GetServerResponse>
    {
        public GetServerRequest(IPAddress address, WebSocket socket) : base(address, socket) { }
    }
}
