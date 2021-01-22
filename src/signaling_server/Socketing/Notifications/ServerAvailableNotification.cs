
namespace signaling_server.Socketing.Notifications
{
    public class ServerAvailableNotification : NotificationBase
    {
        public bool ServerAvailable { get; }
        public string ServerOffer { get; set; }

        public ServerAvailableNotification(string offer) : base(NotificationType.ServerAvailable)
        {
            ServerAvailable = true;
            ServerOffer = offer;
        }
    }
}
