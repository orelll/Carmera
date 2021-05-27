using signaling_server.Requests;
using signaling_server.Responses;
using signaling_server.Socketing;
using signaling_server.Socketing.Notifications;
using signaling_server.Socketing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signaling_server.RequestHandlers
{
    public class NewICEAvailableRequestHandler : IRequestHandler<NewICEAvailableRequest, NewICEAvailableResponse>
    {
        private ISocketRepository _socketRepository;
        private IICERepository _iceRepository;
        private ISocketNotifier _socketNotifier;

        public NewICEAvailableRequestHandler(ISocketRepository socketRepository,
                                            IICERepository iceRepository,
                                            ISocketNotifier socketNotifier)
        {
            _socketRepository = socketRepository;
            _iceRepository = iceRepository;
            _socketNotifier = socketNotifier;
        }
        public NewICEAvailableResponse Handle(NewICEAvailableRequest request)
        {
            var clients = _socketRepository.GetAllClients();

            _iceRepository.AddEntry(request.ICEData);
            var iceNotification = new NewICEAvailableNotification(request.ICEData);

            foreach (var client in clients)
            {
                _socketNotifier.SendMessageAsync(client.Socket, iceNotification);
            }

            return new NewICEAvailableResponse(true);
        }
    }
}
