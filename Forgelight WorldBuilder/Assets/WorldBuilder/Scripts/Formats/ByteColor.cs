namespace WorldBuilder.Formats
{
    using System;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;

    /// <summary>
    /// A Byte value (0-255) represented color.
    /// </summary>
    public struct ByteColor
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public ByteColor(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public ByteColor(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 255;
        }

        public ByteColor(Color color)
        {
            this.r = Convert.ToByte(color.r * 255);
            this.g = Convert.ToByte(color.r * 255);
            this.b = Convert.ToByte(color.r * 255);
            this.a = Convert.ToByte(color.r * 255);
        }

        /// <summary>
        /// Converts the current color value to a native Unity Color.
        /// </summary>
        /// <returns>A Unity float-represented color.</returns>
        public Color ToUnity() => new Color(r/255.0f, g/255.0f, b/255.0f, a/255.0f);
    }

    public enum ColorMemberOrder
    {
        AlphaFirst,
        AlphaLast,
        NoAlpha
    }

    /// <summary>
    /// Stream extensions for reading and writing colors.
    /// </summary>
    public static class ByteColorExtensions
    {
        public static ByteColor ReadByteColor(this Stream stream, ColorMemberOrder order = ColorMemberOrder.AlphaFirst)
        {
            byte x = stream.Read1Byte();
            byte y = stream.Read1Byte();
            byte z = stream.Read1Byte();
            byte w = 0;

            if (order != ColorMemberOrder.NoAlpha)
            {
                w = stream.Read1Byte();
            }

            switch (order)
            {
                case ColorMemberOrder.AlphaFirst:
                    return new ByteColor(y, z, w, x);
                case ColorMemberOrder.AlphaLast:
                    return new ByteColor(x, y, z, w);
                case ColorMemberOrder.NoAlpha:
                    return new ByteColor(x, y, z);
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
        }

        public static void Write(this Stream stream, ByteColor color, ColorMemberOrder order)
        {
            switch (order)
            {
                case ColorMemberOrder.AlphaFirst:
                    stream.Write(color.a);
                    stream.Write(color.r);
                    stream.Write(color.g);
                    stream.Write(color.b);
                    break;
                case ColorMemberOrder.AlphaLast:
                    stream.Write(color.r);
                    stream.Write(color.g);
                    stream.Write(color.b);
                    stream.Write(color.a);
                    break;
                case ColorMemberOrder.NoAlpha:
                    stream.Write(color.r);
                    stream.Write(color.g);
                    stream.Write(color.b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
        }
    }
}