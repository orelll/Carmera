

namespace signaling_server.Responses
{
    public class ClientOfferResponse : ResponseBase
    {
        public bool ServerAvailable { get; set; }
        public string ServerOffer { get; set; }
    }
}
