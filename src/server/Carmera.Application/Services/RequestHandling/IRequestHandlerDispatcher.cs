namespace Carmera.Application.Services.RequestHandling
{
    public interface IRequestHandlerDispatcher
    {
        TOut Dispatch<TOut>(IGenericRequest<TOut> request);
    }
}