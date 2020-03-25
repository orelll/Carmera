using System;
using Carmera.Common;

namespace Carmera.Application.Services.Cache
{
    public interface IRepository<T> where T : class
    {
        Maybe<T> GetOrCreateEntry(CacheKey key, Func<T> predicate);
    }
}