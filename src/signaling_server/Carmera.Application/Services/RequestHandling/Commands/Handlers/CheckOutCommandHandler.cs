using Carmera.Application.Services.Cache;
using Carmera.Application.Services.RequestHandling.Commands.Results;
using Carmera.Common.Common;
using System;

namespace Carmera.Application.Services.RequestHandling.Commands.Handlers
{
    public class CheckOutCommandHandler : CommandHandler<CheckOutCommand, CheckOutCommandResult>
    {
        private readonly IRepository _cacheRepository;

        public CheckOutCommandHandler(IRepository cacheRepository)
        {
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
        }

        public override CheckOutCommandResult Handle(CheckOutCommand request)
        {

            var cacheKey = new StringCacheKey(request.PeerName);
            _cacheRepository.GetOrCreateEntry(cacheKey, () => PrepareCacheEntry(request, cacheKey));

            throw new NotImplementedException();
        }

        private CacheEntry PrepareCacheEntry(CheckOutCommand request, StringCacheKey key)
        {
            var entryValue = new ClientAddress(request.Address, request.Port);
            return new CacheEntry(key, entryValue);
        }
    }
}