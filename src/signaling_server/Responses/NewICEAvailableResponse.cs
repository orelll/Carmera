using signaling_server.Socketing.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signaling_server.Responses
{
    public class NewICEAvailableResponse: ResponseBase
    {
        public bool Success { get; set; }

        public NewICEAvailableResponse(bool success) : base(NotificationType.NewICEAvailable) {
            Success = success;
        }
    }
}
