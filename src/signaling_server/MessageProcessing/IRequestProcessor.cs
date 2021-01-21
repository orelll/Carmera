
using System.Net;

namespace signaling_server.MessageProcessing
{
    public interface IRequestProcessor
    {
        object ProcessRequest(byte[] request, IPAddress address);
    }
}
