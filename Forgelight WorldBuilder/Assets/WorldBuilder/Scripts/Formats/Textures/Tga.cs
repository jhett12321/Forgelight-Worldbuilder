namespace WorldBuilder.Formats.Textures
{
    using System;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;

    public class Tga : IReadableAsset
    {
        public ByteConverter ByteConverter => ByteConverter.Little;

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public short Width;
        public short Height;
        public int BitDepth;
        public Color32[] Data;

        public bool Deserialize(BinaryStream stream, AssetManager assetManager)
        {
            // Skip header
            stream.Seek(12, SeekOrigin.Begin);

            Width = stream.ReadInt16();
            Height = stream.ReadInt16();
            BitDepth = stream.Read1Byte();

            // Skip more header
            stream.Seek(1, SeekOrigin.Current);

            // Data
            Data = new Color32[Width * Height];
            if (BitDepth == 32)
            {
                for (int i = 0; i < Width * Height; i++)
                {
                    byte blue = stream.Read1Byte();
                    byte green = stream.Read1Byte();
                    byte red = stream.Read1Byte();
                    byte alpha = stream.Read1Byte();

                    Data[i] = new Color32(red, green, blue, alpha);
                }
            }
            else if (BitDepth == 24)
            {
                for (int i = 0; i < Width * Height; i++)
                {
                    byte blue = stream.Read1Byte();
                    byte green = stream.Read1Byte();
                    byte red = stream.Read1Byte();

                    Data[i] = new Color32(red, green, blue, 1);
                }
            }
            else
            {
                throw new InvalidOperationException("TGA texture had non 32/24 bit depth.");
            }

            return true;
        }
    }
}