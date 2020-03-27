using System.Net;

namespace Carmera.Common.DTO.Request
{
    public class RequestDTOBase
    {
        public string PeerName { get; set; }
        public IPAddress Address { get; set; }
        public int Port { get; set; }
    }
}