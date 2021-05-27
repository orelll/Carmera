
using System.Runtime.Serialization;

namespace signaling_server.Socketing.Notifications
{
    [DataContract]
    public class ServerAvailableNotification : NotificationBase
    {
        [DataMember]
        public bool ServerAvailable { get; }

        [DataMember]
        public string ServerOffer { get; set; }

        public ServerAvailableNotification(string offer) : base(NotificationType.ServerAvailable)
        {
            ServerAvailable = true;
            ServerOffer = offer;
        }
    }
}
