using Carmera.Application.Services.Cache;
using Carmera.Application.Services.Logging;
using Carmera.Application.Services.RequestHandling.Queries.Results;
using Carmera.Common;
using System;

namespace Carmera.Application.Services.RequestHandling.Queries.Handlers
{
    public class OfferQueryHandler : QueryHandler<OfferQuery, OfferResult>
    {
        private IRepository _repository;
        private ILogger _logger;

        public OfferQueryHandler(IRepository repository, ILogger logger)
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

           
            try
            {
              
                var encodedOfferData = Tools.StringToUTF8ByteArray(request.OfferData);
                
                using (var ws = new WebSocketSharp.WebSocket("ws://10.39.118.63:8080"))
                {
                    ws.OnMessage += (sender, e) =>
                      Console.WriteLine("Laputa says: " + e.Data);

                    ws.Connect();
                    ws.Send("BALUS");
                    Console.ReadKey(true);
                }

                return new OfferResult("DUPSKO");
               
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error during handling offer request: ", ex);
                return new OfferResult("DUPSKO 2"); 
            }
        }
    }
}