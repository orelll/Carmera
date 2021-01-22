using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public abstract class RequestBase<TOut> where TOut: ResponseBase
    {
        public IPAddress Address { get; set; }
        public WebSocket Socket { get; set; }
        public TOut GetResponseType() => default(TOut);

        public RequestBase(IPAddress address, WebSocket socket)
        {
            Address = address;
            Socket = socket;
        }
    }
}
