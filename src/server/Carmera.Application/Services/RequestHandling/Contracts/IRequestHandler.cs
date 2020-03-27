namespace Carmera.Application.Services.RequestHandling.Contracts
{
    public interface IRequestHandler<TReq, TRes> where TReq : IRequest<TRes> where TRes : IResult
    {
        TRes Handle(IRequest<TRes> request);
    }
}