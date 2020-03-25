namespace Carmera.Application.Services.RequestHandling
{
    public interface IRequestHandlingService
    {
        IResponse HandleRequest(IRequest request);
    }
}