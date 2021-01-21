using signaling_server.Responses;
using System.Net;

namespace signaling_server.Requests
{
    public abstract class RequestBase<TOut> where TOut: ResponseBase
    {
        public IPAddress Address { get; set; }
        public TOut GetResponseType() => default(TOut);

        public RequestBase(IPAddress address) => Address = address;
    }
}
