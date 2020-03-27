using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.HandlersDispatcher
{
    public interface IRequestHandlerDispatcher
    {
        IRequestHandler<IRequest<IResult>, IResult> Dispatch(IRequest<IResult> request);
    }
}