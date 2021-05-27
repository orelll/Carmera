
using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public class ClientOfferRequest : RequestBase<ClientOfferResponse>
    {
        public string Offer { get; set; }

        public ClientOfferRequest(IPAddress address, WebSocket socket, string offer) : base(address, socket) => Offer = offer;
    }
}
