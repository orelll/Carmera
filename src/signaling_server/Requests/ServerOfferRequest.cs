using signaling_server.Responses;
using System.Net;

namespace signaling_server.Requests
{
    public class ServerOfferRequest: RequestBase<ServerOfferResponse>
    {
        public string Offer { get; set; }

        public ServerOfferRequest(IPAddress address, string offer) : base(address) => Offer = offer;
    }
}
