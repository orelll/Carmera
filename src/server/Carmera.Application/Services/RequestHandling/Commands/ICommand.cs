namespace Carmera.Application.Services.RequestHandling.Commands
{
    public interface ICommand<TReq, TOut> : IGenericRequest<TOut>
    {
    }
}