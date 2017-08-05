using System;
using System.IO;

namespace WpfAnimatedGif.Decoding
{
    internal class GifDecoder
    {
        internal static GifFile DecodeGif(string path, bool metadataOnly)
        {
            using (var stream = File.OpenRead(path))
            {
                return DecodeGif(stream, metadataOnly);
            }
        }

        internal static GifFile DecodeGif(Stream stream, bool metadataOnly)
        {
            IDisposable bs = null;
            if (!(stream is BufferedStream))
            {
                stream = new BufferedStream(stream);
                bs = stream;
            }

            using (bs)
            {
                return GifFile.ReadGifFile(stream, metadataOnly);
            }
        }
    }
}
