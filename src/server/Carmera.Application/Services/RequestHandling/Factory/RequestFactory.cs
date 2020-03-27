using System;
using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.Query;
using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest<IResult> CreateRequest<TDTO>(TDTO request) where TDTO : RequestDTOBase
        {
            try
            {
                switch (request)
                {
                    case CheckInRequestDTO dto:
                        return ConvertToCheckInCommand(dto);

                    case CheckOutRequestDTO dto:
                        return ConvertToCheckOutCommand(dto);

                    case GetPeerRequestDTO dto:
                        return ConvertToGetPeerCommand(dto);

                    default:
                        throw new ArgumentException(request.GetType().Name);
                }
            }
            catch (Exception e)
            {

                
            }
            return null;
        }

        private IRequest<IResult> ConvertToCheckInCommand(CheckInRequestDTO dto) => new CheckInCommand(dto.PeerName, dto.Address, dto.Port);

        private IRequest<IResult> ConvertToCheckOutCommand(CheckOutRequestDTO dto) => new CheckOutCommand(dto.PeerName, dto.Address, dto.Port);

        private IRequest<IResult> ConvertToGetPeerCommand(GetPeerRequestDTO dto) => new GetPeerQuery(dto.PeerName, dto.Address, dto.Port, dto.SecondSideName);
    }
}