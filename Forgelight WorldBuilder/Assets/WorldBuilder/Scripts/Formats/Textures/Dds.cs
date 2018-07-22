namespace WorldBuilder.Formats.Textures
{
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Utils;

    /// <summary>
    /// Represents a DDS compressed texture.
    /// </summary>
    public class Dds : IReadableAsset
    {
        public ByteConverter ByteConverter => ByteConverter.Little;

        public string Name { get; set; }
        public string DisplayName { get; set; }

        // Header
        private const int HEADER_LENGTH = 128;
        private const byte HEADER_DDS = 124;
        public TextureFormat TextureFormat;

        // Texture Data
        public int Height;
        public int Width;
        public Buffer<byte> TextureData = new Buffer<byte>();

        public bool Deserialize(BinaryStream stream, AssetManager assetManager)
        {
            // Header
            stream.Seek(4, SeekOrigin.Begin);

            Assert.AreEqual(HEADER_DDS, stream.Read1Byte(), "Invalid DDS DXTn texture. Unable to read");

            // Width + Height Data
            stream.Seek(12, SeekOrigin.Begin);

            Height = stream.Read1Byte() + stream.Read1Byte() * 256;

            stream.Seek(16, SeekOrigin.Begin);

            Width = stream.Read1Byte() + stream.Read1Byte() * 256;

            stream.Seek(84, SeekOrigin.Begin);

            // Texture Format
            string texFormat = stream.ReadString(4);
            switch (texFormat)
            {
                case "DXT5":
                    TextureFormat = TextureFormat.DXT5;
                    break;
                case "DXT1":
                    TextureFormat = TextureFormat.DXT1;
                    break;
                case "DXT3":
                    TextureFormat = TextureFormat.DXT5;
                    break;
                default:
                    Debug.LogWarning("Unknown Texture type: " + texFormat);
                    return false;
            }

            // Texture Data
            stream.Seek(HEADER_LENGTH, SeekOrigin.Begin);
            stream.ReadBytes(TextureData, (int)stream.Length - HEADER_LENGTH);

            return true;
        }
    }
}