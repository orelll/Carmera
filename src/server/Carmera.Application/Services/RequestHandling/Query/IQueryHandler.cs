using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Query
{
    public interface IQueryHandler<TReq, TRes> : IRequestHandler<TReq, TRes> where TReq : IRequest<TRes> where TRes : IResult
    {
    }
}