using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;

namespace signaling_server.RequestHandlers
{
    public class AnswerRequestHandler : IRequestHandler<AnswerRequest, AnswerResponse>
    {
        private readonly ISocketRepository _socketRepository;
        private readonly ISocketNotifier _notifier;

        public AnswerRequestHandler(ISocketRepository socketRepository, ISocketNotifier notifier)
        {
            _socketRepository = socketRepository;
            _notifier = notifier;
        }

        public AnswerResponse Handle(AnswerRequest request)
        {
            if (_socketRepository.ContainsServer())
            {
                var serverSocket = _socketRepository.GetServer();

                _notifier.SendAnswerToServer(serverSocket, request.AnswerOffer);

                return new AnswerResponse("Processed", true);
            }
            else {
                return new AnswerResponse("Server not found", false);
            }

        }
    }
}
