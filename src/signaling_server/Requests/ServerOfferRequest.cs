using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public class ServerOfferRequest: RequestBase<ServerOfferResponse>
    {
        public string Offer { get; set; }

        public ServerOfferRequest(IPAddress address, WebSocket socket, string offer) : base(address, socket) => Offer = offer;
    }
}
