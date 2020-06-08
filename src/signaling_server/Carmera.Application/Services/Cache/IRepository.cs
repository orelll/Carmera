using Carmera.Common;
using System;

namespace Carmera.Application.Services.Cache
{
    public interface IRepository
    {
        Maybe<CacheEntry> GetOrCreateEntry(CacheKey key, Func<CacheEntry> predicate);

        Maybe<CacheEntry> GetEntry(CacheKey key);
    }
}