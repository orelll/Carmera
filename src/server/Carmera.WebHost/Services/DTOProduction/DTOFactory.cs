using System;
using Carmera.Application.Services.RequestHandling;
using Carmera.Common.DTO.Request;
using Newtonsoft.Json;

namespace Carmera.WebHost.Services.DTOProduction
{
    public class DTOFactory : IDTOFactory
    {
        public RequestDTOBase ObtainDTO(RequestsTypes.RequestType requestType, PeerInfo peerInfo)
        {
            RequestDTOBase deserializedRequest = null;

            switch (requestType)
            {
                case RequestsTypes.RequestType.CheckIn:
                    deserializedRequest = JsonConvert.DeserializeObject<CheckInRequestDTO>(peerInfo.Payload);
                    break;

                case RequestsTypes.RequestType.CheckOut:
                    deserializedRequest = JsonConvert.DeserializeObject<CheckOutRequestDTO>(peerInfo.Payload);
                    break;

                case RequestsTypes.RequestType.GetPeer:
                    deserializedRequest = JsonConvert.DeserializeObject<GetPeerRequestDTO>(peerInfo.Payload);
                    break;

                default:
                    throw new ArgumentException("Request type not handled");
            }

            return FulfillPeerData(deserializedRequest, peerInfo);
        }

        private RequestDTOBase FulfillPeerData(RequestDTOBase request, PeerInfo peerInfo)
        {
            request.Address = peerInfo.Address;
            request.Port = peerInfo.Port;

            return request;
        }
    }
}