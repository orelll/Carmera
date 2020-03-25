namespace Carmera.Common
{
    public class Maybe<T>
    {
        private readonly T _value;
        public T Value { get => _value; }
        public bool HasValue => _value != null;

        private Maybe(T value) => _value = value;

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);
    }
}