using signaling_server.Requests;
using signaling_server.Responses;

namespace signaling_server.RequestHandlers
{
    public class TxtRequestHandler : IRequestHandler<TxtRequest, TxtResponse>
    {
        public TxtResponse Handle(TxtRequest request)
        {
            return new TxtResponse { Message = "processed" };
        }
    }
}
