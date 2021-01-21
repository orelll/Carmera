using signaling_server.Requests;
using signaling_server.Responses;

namespace signaling_server.RequestHandlers
{
    public interface IRequestHandler<TIn, TOut> where TIn: RequestBase<TOut> where TOut: ResponseBase
    {
        TOut Handle(TIn request);
    }
}
