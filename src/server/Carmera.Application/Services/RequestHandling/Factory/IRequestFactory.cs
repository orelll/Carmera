using Carmera.Common.DTO.Request;
using Carmera.Common.DTO.Response;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public interface IRequestFactory
    {
        IGenericRequest<ResponseDTOBase<TIn, TOut>> CreateRequest<TIn, TOut>(TIn request) where TIn: RequestDTOBase;
    }
}