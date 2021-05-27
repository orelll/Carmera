
using System;
using System.Runtime.Serialization;

namespace signaling_server.Socketing.Notifications
{
    [DataContract]
    public abstract class NotificationBase
    {
        [DataMember]
        string ResponseType { get; }

        public NotificationBase(NotificationType type) => ResponseType = Enum.GetName(typeof(NotificationType), type);
    }
}
