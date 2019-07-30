using System.Collections.Specialized;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace LokiRAT
{
    class utils
    {
        public static byte[] GetStringToBytes(string value)
        {
            SoapHexBinary shb = SoapHexBinary.Parse(value);
            return shb.Value;
        }

        public static string GetBytesToString(byte[] value)
        {
            SoapHexBinary shb = new SoapHexBinary(value);
            return shb.ToString();
        }
    }
}