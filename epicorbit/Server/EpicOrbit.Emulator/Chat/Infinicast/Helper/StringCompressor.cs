using System.IO;
using System.IO.Compression;
using System.Text;

namespace EpicOrbit.Emulator.Chat.Infinicast.Helper {
    public static class StringCompressor {

        public static string Decompress(byte[] input) {
            using (MemoryStream memoryStream = new MemoryStream(input))
            using (MemoryStream resultStream = new MemoryStream())
            using (GZipStream compressionStream = new GZipStream(memoryStream, CompressionMode.Decompress)) {
                compressionStream.CopyTo(resultStream);
                return Encoding.UTF8.GetString(resultStream.ToArray());
            }
        }

    }
}
