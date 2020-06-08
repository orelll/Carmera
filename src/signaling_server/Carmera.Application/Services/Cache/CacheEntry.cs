namespace Carmera.Application.Services.Cache
{
    public class CacheEntry
    {
        public CacheKey Key { get; }
        public object Value { get; }

        public CacheEntry(CacheKey key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}