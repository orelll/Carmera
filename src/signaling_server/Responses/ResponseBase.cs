using signaling_server.Socketing.Notifications;
using System;

namespace signaling_server.Responses
{

    public abstract class ResponseBase
    {
        public string ResponseType { get; }

        public ResponseBase(NotificationType responseType) => ResponseType = Enum.GetName(typeof(NotificationType), responseType);
    }
}
