namespace Carmera.Application.Services.Cache
{
    public class StringCacheKey : CacheKey
    {
        public string Value { get; set; }

        public StringCacheKey(string value) => Value = value;
    }
}