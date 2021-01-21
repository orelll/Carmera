
using signaling_server.Responses;
using System.Net;

namespace signaling_server.Requests
{
    public class TxtRequest : RequestBase<TxtResponse>
    {
        public string Message { get; set; }

        public TxtRequest(IPAddress address, string message) : base(address) => Message = message;
    }
}
