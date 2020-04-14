using System.Text;

namespace Carmera.Common
{
    public static class Tools
    {
        public static byte[] StringToUTF8ByteArray(string text) => Encoding.UTF8.GetBytes(text);
        public static string BytesArrayToString(byte[] bytes) => Encoding.UTF8.GetString(bytes);
    }
}