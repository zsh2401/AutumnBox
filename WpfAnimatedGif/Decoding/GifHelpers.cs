using System;
using System.IO;
using System.Text;

namespace WpfAnimatedGif.Decoding
{
    internal static class GifHelpers
    {
        internal static string ReadString(Stream stream, int length)
        {
            byte[] bytes = new byte[length];
            stream.Read(bytes, 0, length);
            return Encoding.ASCII.GetString(bytes);
        }

        internal static byte[] ReadDataBlocks(Stream stream, bool discard)
        {
            MemoryStream ms = discard ? null : new MemoryStream();
            using (ms)
            {
                int len;
                while ((len = stream.ReadByte()) > 0)
                {
                    byte[] bytes = new byte[len];
                    stream.Read(bytes, 0, len);
                    if (ms != null)
                        ms.Write(bytes, 0, len);
                }
                if (ms != null)
                    return ms.ToArray();
                return null;
            }
        }

        internal static GifColor[] ReadColorTable(Stream stream, int size)
        {
            int length = 3 * size;
            byte[] bytes = new byte[length];
            stream.Read(bytes, 0, length);
            GifColor[] colorTable = new GifColor[size];
            for (int i = 0; i < size; i++)
            {
                byte r = bytes[3 * i];
                byte g = bytes[3 * i + 1];
                byte b = bytes[3 * i + 2];
                colorTable[i] = new GifColor(r, g, b);
            }
            return colorTable;
        }

        internal static Exception UnexpectedEndOfStreamException()
        {
            return new GifDecoderException("Unexpected end of stream before trailer was encountered");
        }

        internal static Exception UnknownBlockTypeException(int blockId)
        {
            return new GifDecoderException("Unknown block type: 0x" + blockId.ToString("x2"));
        }

        internal static Exception UnknownExtensionTypeException(int extensionLabel)
        {
            return new GifDecoderException("Unknown extension type: 0x" + extensionLabel.ToString("x2"));
        }

        internal static Exception InvalidBlockSizeException(string blockName, int expectedBlockSize, int actualBlockSize)
        {
            return new GifDecoderException(
                string.Format(
                    "Invalid block size for {0}. Expected {1}, but was {2}",
                    blockName,
                    expectedBlockSize,
                    actualBlockSize));
        }

        public static Exception InvalidSignatureException(string signature)
        {
            return new GifDecoderException("Invalid file signature: " + signature);
        }

        public static Exception UnsupportedVersionException(string version)
        {
            return new GifDecoderException("Unsupported version: " + version);
        }
    }
}
