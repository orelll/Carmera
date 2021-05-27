using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public class NewICEAvailableRequest:RequestBase<NewICEAvailableResponse>
    {
        public string ICEData { get; set; }

        public NewICEAvailableRequest(IPAddress address, WebSocket socket, string iceData) : base(address, socket) => ICEData = iceData;
    }
}
