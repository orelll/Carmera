using Carmera.Application.Entities;
using Carmera.Application.Services.Cache;
using Carmera.Application.Services.Logging;
using Carmera.Application.Services.RequestHandling.Queries.Results;
using Carmera.Common;
using System;
using System.Net.Sockets;

namespace Carmera.Application.Services.RequestHandling.Queries.Handlers
{
    public class OfferQueryHandler : QueryHandler<OfferQuery, OfferResult>
    {
        private IRepository<ClientInfo> _repository;
        private ILogger _logger;

        public OfferQueryHandler(IRepository<ClientInfo> repository, ILogger logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override OfferResult Handle(OfferQuery request)
        {
            // validate if offer data is not empty
            // look for carmera client
            // call this client with offer data payload
            // return answer
            if (string.IsNullOrEmpty(request?.OfferData)) throw new ArgumentException(nameof(request.OfferData));

            var carmeraPeerKey = new StringCacheKey("carmera");
            var carmeraPeer = _repository.GetEntry(carmeraPeerKey);

            if (!carmeraPeer.HasValue) throw new ArgumentNullException(nameof(carmeraPeer));

            var peerClient = new TcpClient();

            try
            {
                peerClient.Connect(carmeraPeer.Value.Address, carmeraPeer.Value.Port);

                using (var str = peerClient.GetStream())
                {
                    var encodedOfferData = Tools.StringToUTF8ByteArray(request.OfferData);
                    str.Write(encodedOfferData, 0, encodedOfferData.Length);

                    while (!str.CanRead) { }

                    var responseBuffer = new byte[peerClient.ReceiveBufferSize];
                    str.Read(responseBuffer, 0, peerClient.ReceiveBufferSize);

                    var offerAnswer = Tools.BytesArrayToString(responseBuffer);

                    return new OfferResult(offerAnswer);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error during handling offer request: ", ex);
            }

            throw new NotImplementedException();
        }
    }
}