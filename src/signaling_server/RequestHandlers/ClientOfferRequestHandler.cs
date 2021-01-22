using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;

namespace signaling_server.RequestHandlers
{
    public class ClientOfferRequestHandler : IRequestHandler<ClientOfferRequest, ClientOfferResponse>
    {
        private readonly ISocketRepository _socketRepository;
        public ClientOfferRequestHandler(ISocketRepository socketRepository) 
        {
            _socketRepository = socketRepository;
        }

        public ClientOfferResponse Handle(ClientOfferRequest request)
        {
            var socketData = new SocketData(request.Socket, SocketKind.client, request.Address, request.Offer);
            _socketRepository.OnSocketConnected(socketData);

            var serverAvailable = _socketRepository.ContainsServer();
            var serverOffer = _socketRepository.GetServer()?.Offer ?? string.Empty;

            return new ClientOfferResponse(serverAvailable, serverOffer);
        }
    }
}
