using System;
using Carmera.Application.Services.Logging;
using Carmera.Common;
using Carmera.Common.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Carmera.Application.Services.Cache
{
    public class CacheRepository : IRepository
    {
        private IMemoryCache _cache;
        private ILogger _logger;

        public CacheRepository(IMemoryCache memoryCache, ILogger logger)
        {
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Maybe<CacheEntry> GetEntry(CacheKey key)
        {
             _cache.TryGetValue(key, out Maybe<CacheEntry> cacheEntry);
            return cacheEntry;
        }

        public Maybe<CacheEntry> GetOrCreateEntry(CacheKey key, Func<CacheEntry> createItem)
        {
            _cache.TryGetValue(key, out Maybe<CacheEntry> cacheEntry);

            if (!cacheEntry?.HasValue != true)
            {
                try
                {
                    var x = createItem();
                    cacheEntry = x.ToMaybe();
                    _cache.Set(key, cacheEntry);
                }
                catch (Exception ex)
                {
                    _logger.Error("There was a problem during caching mechanism usage.", ex);
                }
            }

            return cacheEntry;
        }
    }
}