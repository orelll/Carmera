using System;

namespace Carmera.Application.Services.Cache
{
    public interface IRepository<T>
    {
        T GetOrCreateEntry(CacheKey key, Func<T> predicate);
    }
}