
using signaling_server.Socketing.Notifications;

namespace signaling_server.Responses
{
    public class TxtResponse : ResponseBase
    {
        public string Message { get; }

        public TxtResponse(string message) : base(NotificationType.TxtResponse) 
        {
            Message = message;
        }
    }
}
