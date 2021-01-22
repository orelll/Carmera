using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;
using System.Linq;

namespace signaling_server.RequestHandlers
{
    public class ServerOfferRequestHandler : IRequestHandler<ServerOfferRequest, ServerOfferResponse>
    {
        private readonly ISocketRepository _socketRepository;
        private readonly ISocketNotifier _notifier;
        public ServerOfferRequestHandler(ISocketRepository socketRepository, ISocketNotifier notifier)
        {
            _socketRepository = socketRepository;
            _notifier = notifier;
        }

        public ServerOfferResponse Handle(ServerOfferRequest request)
        {
            var socketData = new SocketData(request.Socket, SocketKind.server, request.Address, request.Offer);
            _socketRepository.OnSocketConnected(socketData);

            NotifyAllClients(socketData);

            return new ServerOfferResponse("Processed");
        }

        private void NotifyAllClients(SocketData server) {

            foreach (var client in _socketRepository.GetAllClients().Where(c => !c.Notified))
            {
                _notifier.SendServerAvailable(client, server.Offer);
                client.Notified = true;
            }
        }
    }
}
