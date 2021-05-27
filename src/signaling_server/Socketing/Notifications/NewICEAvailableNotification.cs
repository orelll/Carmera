using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace signaling_server.Socketing.Notifications
{
    [DataContract]
    public class NewICEAvailableNotification : NotificationBase
    {
        [DataMember]
        public string ICEData { get; set; }

        public NewICEAvailableNotification(string iceData) : base(NotificationType.NewICEAvailable) => ICEData = iceData;
    }
}
