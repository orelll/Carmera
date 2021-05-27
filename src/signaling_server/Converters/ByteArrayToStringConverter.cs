using System.Text;

namespace signaling_server.Converters
{
    public class ByteArrayToStringConverter: IConvert<byte[], string>
    {
        public string Convert(byte[] input)
        {
            return input == null ? string.Empty : Encoding.ASCII.GetString(input);
        }
    }
}
