namespace Carmera.Application.Services.Cache
{
    public class StringCacheKey : CacheKey
    {
        public string Value { get; set; }

        public StringCacheKey(string value) => Value = value;

        public override bool Equals(object obj)
        {
            return ((StringCacheKey)obj).Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}