using Carmera.Application.Services.RequestHandling;
using Carmera.Common.DTO.Request;

namespace Carmera.WebHost.Services.DTOProduction
{
    public interface IDTOFactory
    {
        RequestDTOBase ObtainDTO(RequestsTypes.RequestType requestType, PeerInfo peerInfo);
    }
}