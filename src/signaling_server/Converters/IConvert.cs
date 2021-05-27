
namespace signaling_server.Converters
{
    public interface IConvert<TIn, TOut>
    {
        TOut Convert(TIn input);
    }
}
