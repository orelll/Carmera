namespace Carmera.Application.Services.RequestHandling
{
    public class RequestsTypes
    {
        public enum RequestType
        {
            Unknown,
            CheckIn,
            CheckOut,
            GetPeer,
            ListPeers,
            Offer
        }
    }
}