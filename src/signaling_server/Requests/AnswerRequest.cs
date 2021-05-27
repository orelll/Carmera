using signaling_server.Responses;
using System.Net;
using System.Net.WebSockets;

namespace signaling_server.Requests
{
    public class AnswerRequest : RequestBase<AnswerResponse>
    {
        public string AnswerOffer { get; set; }

        public AnswerRequest(IPAddress address, WebSocket socket, string answerOffer) : base(address, socket)
        { 
            AnswerOffer = answerOffer;
        }
    }
}
