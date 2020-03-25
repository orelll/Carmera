using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public interface IRequestFactory
    {
        IRequest CreateRequest<TReq>(TReq request) where TReq : RequestDTOBase;
    }
}