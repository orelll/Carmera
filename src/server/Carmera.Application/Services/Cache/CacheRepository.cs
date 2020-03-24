using System;

namespace Carmera.Application.Services.Cache
{
    public class CacheRepository<T> : IRepository<T>
    {
        public T GetOrCreateEntry(CacheKey key, Func<T> predicate)
        {
            throw new NotImplementedException();
        }
    }
}