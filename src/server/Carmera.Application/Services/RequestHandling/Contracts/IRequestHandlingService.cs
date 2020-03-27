using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling
{
    public interface IRequestHandlingService
    {
        IResult HandleRequest(IRequest<IResult> request);
    }
}