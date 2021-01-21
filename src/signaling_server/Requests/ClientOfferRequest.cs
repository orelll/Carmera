
using signaling_server.Responses;
using System.Net;

namespace signaling_server.Requests
{
    public class ClientOfferRequest : RequestBase<ClientOfferResponse>
    {
        public string Offer { get; set; }

        public ClientOfferRequest(IPAddress address, string offer) : base(address) => Offer = offer;
    }
}
