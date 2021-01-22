
using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public class TxtRequest : RequestBase<TxtResponse>
    {
        public string Message { get; set; }

        public TxtRequest(IPAddress address, WebSocket socket, string message) : base(address, socket) => Message = message;
    }
}
