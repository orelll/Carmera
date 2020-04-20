using System.Threading.Tasks;

namespace Carmera.Application.Services.RequestHandling.Contracts
{
    public abstract class RequestHandler<TReq, TRes> where TReq : Request where TRes : Result
    {
        abstract public TRes Handle(TReq request);
    }
}