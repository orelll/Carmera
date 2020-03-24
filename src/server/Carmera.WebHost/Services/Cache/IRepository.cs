using System;

namespace Carmera.WebHost.Services.Cache
{
    public interface IRepository<T>
    {
        T GetOrCreateEntry(CacheKey key, Func<T> predicate);
    }
}