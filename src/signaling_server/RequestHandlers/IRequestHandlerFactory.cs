using signaling_server.Requests;
using signaling_server.Responses;

namespace signaling_server.RequestHandlers
{
    public interface IRequestHandlerFactory
    {
        IRequestHandler<TIn, TOut> Create<TIn, TOut>(TIn requestType, TOut responseType) where TIn: RequestBase<TOut> where TOut: ResponseBase;
    }
}
