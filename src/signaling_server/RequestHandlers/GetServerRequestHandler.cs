using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;

namespace signaling_server.RequestHandlers
{
    public class GetServerRequestHandler : IRequestHandler<GetServerRequest, GetServerResponse>
    {
        private readonly ISocketRepository _socketRepository;
        public GetServerRequestHandler(ISocketRepository socketRepository)
        {
            _socketRepository = socketRepository;
        }

        public GetServerResponse Handle(GetServerRequest request)
        {
            var serverAvailable = _socketRepository.ContainsServer();
            var serverOffer = _socketRepository.GetServer()?.Offer ?? string.Empty;

            return new GetServerResponse(serverAvailable, serverOffer);
        }
    }
}
