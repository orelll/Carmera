using System.Threading.Tasks;

namespace Carmera.Application.Services.RequestHandling.Contracts
{
    public abstract class RequestHandler<TReq, TRes> where TReq : Request where TRes : Result
    {
        abstract public Task<TRes> HandleAsync(TReq request);
    }
}