
using signaling_server.Socketing.Notifications;

namespace signaling_server.Responses
{
    public class GetServerResponse: ResponseBase
    {
        public bool ServerAvailable { get; }
        public string ServerOffer { get; }

        public GetServerResponse(bool serverAvailable, string serverOffer) : base(NotificationType.GetServerResponse) 
        {
            ServerAvailable = serverAvailable;
            ServerOffer = serverOffer;
        }
    }
}
