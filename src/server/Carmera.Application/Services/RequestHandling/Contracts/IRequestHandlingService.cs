using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling
{
    public interface IRequestHandlingService
    {
        Result HandleRequest<TReq>(TReq request) where TReq : Request;
    }
}