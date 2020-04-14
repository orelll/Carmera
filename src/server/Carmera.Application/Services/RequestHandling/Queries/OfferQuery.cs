using System.Net;

namespace Carmera.Application.Services.RequestHandling.Queries
{
    public class OfferQuery : QueryBase
    {
        public string OfferData { get; private set; }

        public OfferQuery(string peerName, IPAddress address, int port, string offerData) : base(peerName, address, port)
        {
            OfferData = offerData;
        }
    }
}