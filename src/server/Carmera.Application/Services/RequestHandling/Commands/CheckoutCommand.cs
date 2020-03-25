using Carmera.Common.DTO.Request;
using Carmera.Common.DTO.Response;

namespace Carmera.Application.Services.RequestHandling.Commands
{
    public class CheckoutCommand : ICommand<CheckoutRequestDTO, ResponseDTOBase<CheckoutRequestDTO, bool>>
    {
    }
}