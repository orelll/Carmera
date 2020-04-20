using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Application.Services.RequestHandling.Contracts;
using Carmera.Application.Services.RequestHandling.Queries;
using Carmera.Common.DTO.Request;
using System;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public class RequestFactory : IRequestFactory
    {
        public Request CreateRequest<TDTO>(TDTO request) where TDTO : RequestDTOBase
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
                        
                    case OfferRequestDTO dto:
                        return ConvertToOfferCommand(dto);

                    default:
                        throw new ArgumentException(request.GetType().Name);
                }
            }
            catch (Exception e)
            {
            }
            return null;
        }

        private Request ConvertToCheckInCommand(CheckInRequestDTO dto) => new CheckInCommand(dto.PeerName, dto.Address, dto.Port);

        private Request ConvertToCheckOutCommand(CheckOutRequestDTO dto) => new CheckOutCommand(dto.PeerName, dto.Address, dto.Port);

        private Request ConvertToGetPeerCommand(GetPeerRequestDTO dto) => new GetPeerQuery(dto.PeerName, dto.Address, dto.Port, dto.SecondSideName);
        private Request ConvertToOfferCommand(OfferRequestDTO dto) => new OfferQuery(dto.PeerName, dto.Address, dto.Port, dto.OfferData);
    }
}