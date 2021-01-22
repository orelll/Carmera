
namespace signaling_server.Socketing.Notifications
{
    public class PeerAnswerNotification: NotificationBase
    {
        public string AnswerOffer { get; }

        public PeerAnswerNotification(string offer) : base(NotificationType.Answer) => AnswerOffer = offer;
    }
}
