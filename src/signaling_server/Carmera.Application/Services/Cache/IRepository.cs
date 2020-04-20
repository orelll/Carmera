using Carmera.Common;
using System;

namespace Carmera.Application.Services.Cache
{
    public interface IRepository<T> where T : class
    {
        Maybe<T> GetOrCreateEntry(CacheKey key, Func<T> predicate);

        Maybe<T> GetEntry(CacheKey key);
    }
}