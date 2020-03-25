using Carmera.Common.DTO.Response;

namespace Carmera.Application.Services.RequestHandling
{
    public interface IRequestHandlerDispatcher
    {
        ResponseDTOBase Dispatch(IRequest request);
    }
}