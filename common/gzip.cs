using System;
using System.IO;
using System.IO.Compression;

namespace common
{
    public class gzip
    {
        public static byte[] Compress(byte[] data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (GZipStream gzs = new GZipStream(ms, CompressionMode.Compress))
                    {
                        gzs.Write(data, 0, data.Length);
                        gzs.Close();
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex) { return null; }
        }

        public static byte[] Decompress(byte[] data)
        {
            try
            {
                using (MemoryStream dms = new MemoryStream())
                {
                    using (GZipStream gzs = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int n;
                        while ((n = gzs.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            dms.Write(bytes, 0, n);
                        }
                        gzs.Close();
                    }
                    return dms.ToArray();
                }
            }
            catch (Exception ex) { return null; }
        }
    }
}