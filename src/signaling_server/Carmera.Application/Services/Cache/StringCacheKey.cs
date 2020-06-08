namespace Carmera.Application.Services.Cache
{
    public class StringCacheKey : CacheKey
    {
        public string Key { get; set; }

        public StringCacheKey(string key) => Key = key;

        public override bool Equals(object obj)
        {
            return (obj as StringCacheKey)?.Key == Key;
        }

        public override int GetHashCode()
        {
            return Key?.GetHashCode() ?? 0;
        }
    }
}