
using signaling_server.Socketing.Notifications;

namespace signaling_server.Responses
{
    public class AnswerResponse : ResponseBase
    {
        public string Message { get; }
        public bool Success { get; }

        public AnswerResponse(string message, bool success): base(NotificationType.AnswerResponse)
        {
            Message = message;
            Success = success;
        } 
    }
}
