using Carmera.Common;
using Carmera.Common.DTO.Request;

namespace Carmera.WebHost.Services.DTOProduction
{
    public interface IDTOFactory
    {
        RequestDTOBase ObtainDTO(string message);
    }
}