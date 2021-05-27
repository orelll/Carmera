
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace signaling_server.Socketing.Notifications
{
    [DataContract]
    public class PeerAnswerNotification: NotificationBase
    {
        [DataMember]
        public string AnswerOffer { get; }

        public PeerAnswerNotification(string offer) : base(NotificationType.Answer) => AnswerOffer = offer;
    }
}
