

using signaling_server.Socketing.Notifications;

namespace signaling_server.Responses
{
    public class ClientOfferResponse : ResponseBase
    {
        public bool ServerAvailable { get; }
        public string ServerOffer { get; }

        public ClientOfferResponse(bool serverAvailable, string serverOffer) : base(NotificationType.ClientOfferResponse)
        {

            ServerOffer = serverOffer;
            ServerAvailable = serverAvailable;
        }
    }
}
