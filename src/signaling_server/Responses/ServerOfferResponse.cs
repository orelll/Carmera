using signaling_server.Socketing.Notifications;

namespace signaling_server.Responses
{
    public class ServerOfferResponse: ResponseBase
    {
        public string Message { get; }

        public ServerOfferResponse(string message): base(NotificationType.ServerOfferResponse)
        {
            Message = message;
        }
    }
}
