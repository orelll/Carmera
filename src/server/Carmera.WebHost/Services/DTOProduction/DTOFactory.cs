using System;
using Carmera.Application.Services.RequestHandling;
using Carmera.Common.DTO.Request;

namespace Carmera.WebHost.Services.DTOProduction
{
    public class DTOFactory : IDTOFactory
    {
        public RequestDTOBase ObtainDTO(RequestsTypes.RequestType requestType, string message)
        {
            throw new NotImplementedException();
        }
    }
}