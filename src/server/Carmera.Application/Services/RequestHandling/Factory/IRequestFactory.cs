using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public interface IRequestFactory
    {
        IRequest<IResult> CreateRequest<TDTO>(TDTO request) where TDTO : RequestDTOBase;
    }
}