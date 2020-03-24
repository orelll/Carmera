using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carmera.WebHost.Services.Cache
{
    public class CacheRepository<T> : IRepository<T>
    {
        public T GetOrCreateEntry(CacheKey key, Func<T> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
