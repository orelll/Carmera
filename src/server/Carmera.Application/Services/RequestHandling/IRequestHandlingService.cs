namespace Carmera.Application.Services.RequestHandling
{
    public interface IRequestHandlingService
    {
        TOut HandleRequest<TOut>(IGenericRequest<TOut> request);
    }
}