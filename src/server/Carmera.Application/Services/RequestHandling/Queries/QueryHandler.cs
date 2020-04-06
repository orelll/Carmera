using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Queries
{
    public abstract class QueryHandler<TReq, TRes> : RequestHandler<TReq, TRes> where TReq : Request where TRes : Result
    {
    }
}