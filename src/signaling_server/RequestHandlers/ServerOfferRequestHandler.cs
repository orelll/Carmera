using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;

namespace signaling_server.RequestHandlers
{
    public class ServerOfferRequestHandler : IRequestHandler<ServerOfferRequest, ServerOfferResponse>
    {
        private readonly ISocketRepository _socketRepository;
        public ServerOfferRequestHandler(ISocketRepository socketRepository)
        {
            _socketRepository = socketRepository;
        }

        public ServerOfferResponse Handle(ServerOfferRequest request)
        {
            var socketData = new SocketData { Address = request.Address, Offer = request.Offer, SocketKind = SocketKind.server };
            _socketRepository.OnSocketConnected(socketData);
            return new ServerOfferResponse { Message = "Processed" };
        }
    }
}
