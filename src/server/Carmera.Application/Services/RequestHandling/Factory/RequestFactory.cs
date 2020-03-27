using System;
using Carmera.Application.Services.RequestHandling.Commands;
using Carmera.Common.DTO.Request;

namespace Carmera.Application.Services.RequestHandling.Factory
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest CreateRequest<TIn>(TIn request) where TIn : RequestDTOBase
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

        private CheckInCommand ConvertToCheckInCommand(CheckInRequestDTO dto) => new CheckInCommand(dto.PeerName, dto.Address, dto.Port);

        private CheckOutCommand ConvertToCheckOutCommand(CheckOutRequestDTO dto) => new CheckOutCommand(dto.PeerName, dto.Address, dto.Port);

        private GetPeerCommand ConvertToGetPeerCommand(GetPeerRequestDTO dto) => new GetPeerCommand(dto.PeerName, dto.Address, dto.Port, dto.SecondSideName);
    }
}