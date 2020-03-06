using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Tools
{
    public static class ZipHelper
    {
        public static string Unzip(byte[] zippedBuffer)
        {
            string r = string.Empty;
            using (var zippedStream = new MemoryStream(zippedBuffer))
            {
                using (var archive = new ZipArchive(zippedStream))
                {
                    var entry = archive.Entries.FirstOrDefault();

                    if (entry != null)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var ms = new MemoryStream())
                            {
                                unzippedEntryStream.CopyTo(ms);
                                var unzippedArray = ms.ToArray();

                                r = Encoding.Default.GetString(unzippedArray);
                            }
                        }
                    }
                }
            }
            return r;
        }


        public static byte[] UnzipToStream(byte[] zippedBuffer)
        {
            byte[] r=null;
            using (var zippedStream = new MemoryStream(zippedBuffer))
            {
                using (var archive = new ZipArchive(zippedStream))
                {
                    var entry = archive.Entries.FirstOrDefault();

                    if (entry != null)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var ms = new MemoryStream())
                            {
                                unzippedEntryStream.CopyTo(ms);
                                r = ms.ToArray();
                            }
                        }
                    }
                }
            }
            return r;
        }
    }
}