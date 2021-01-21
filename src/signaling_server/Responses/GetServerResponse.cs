
namespace signaling_server.Responses
{
    public class GetServerResponse: ResponseBase
    {
        public bool ServerAvailable { get; set; }
        public string ServerOffer { get; set; }
    }
}
