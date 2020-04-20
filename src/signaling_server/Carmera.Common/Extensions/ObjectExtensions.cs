namespace Carmera.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value) where T : class => (Maybe<T>)(value);
    }
}