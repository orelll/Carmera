using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public interface ICommandHandler<TReq, TRes> : IRequestHandler<TReq, TRes> where TReq : IRequest<TRes> where TRes: IResult
    {
    }
}