using Carmera.Application.Services.RequestHandling.Contracts;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public abstract class CommandHandler<TReq, TRes> : RequestHandler<TReq, TRes> where TReq: Request where TRes: Result
    {
    }
}