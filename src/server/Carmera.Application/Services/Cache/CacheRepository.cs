using System;
using Carmera.Application.Services.Logging;
using Carmera.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Carmera.Application.Services.Cache
{
    public class CacheRepository<T> : IRepository<T> where T: class
    {
        private IMemoryCache _cache;
        private ILogger _logger;

        public CacheRepository(IMemoryCache memoryCache, ILogger logger)
        {
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Maybe<T> GetOrCreateEntry(CacheKey key, Func<T> createItem)
        {
            _cache.TryGetValue(key, out Maybe<T> cacheEntry);

            if (!cacheEntry.HasValue)
            {
                try
                {
                    var x = createItem();
                    cacheEntry = x as Maybe<T>;
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