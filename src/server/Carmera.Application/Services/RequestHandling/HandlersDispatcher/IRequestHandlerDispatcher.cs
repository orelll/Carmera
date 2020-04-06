using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.HandlersDispatcher
{
    public interface IRequestHandlerDispatcher
    {
        RequestHandler<TReq, Result> Dispatch<TReq>(TReq request) where TReq : Request;
    }
}