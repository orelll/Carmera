
using System;

namespace signaling_server.Socketing.Notifications
{
    public abstract class NotificationBase
    {
        string ResponseType { get; }

        public NotificationBase(NotificationType type) => ResponseType = Enum.GetName(typeof(NotificationType), type);
    }
}
