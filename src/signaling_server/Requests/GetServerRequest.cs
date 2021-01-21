using signaling_server.Responses;
using System.Net;

namespace signaling_server.Requests
{
    public class GetServerRequest:RequestBase<GetServerResponse>
    {
        public GetServerRequest(IPAddress address) : base(address) { }
    }
}
