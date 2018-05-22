namespace WorldBuilder.Formats.Zone
{
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;

    public enum LightType
    {
        Pointlight = 1,
        Spotlight = 2,
    }

    public class Light
    {
        #region Structure
        public string Name { get; set; }
        public string ColorName { get; set; }
        public LightType Type { get; set; }
        public float UnknownFloat1 { get; set; }
        public Vector4 Position { get; set; }
        public Vector4 Rotation { get; set; }
        public float Range { get; set; }
        public float InnerRange { get; set; }
        public ByteColor Color { get; set; }
        public byte UnknownByte1 { get; set; }
        public byte UnknownByte2 { get; set; }
        public byte UnknownByte3 { get; set; }
        public byte UnknownByte4 { get; set; }
        public byte UnknownByte5 { get; set; }
        public Vector4 UnknownVector1 { get; set; }
        public string UnknownString1 { get; set; }
        public uint ID { get; set; }
        #endregion

        public static Light ReadFromStream(Stream stream)
        {
            Light light = new Light();

            light.Name = stream.ReadString(StringCoding.ZeroTerminated);
            light.ColorName = stream.ReadString(StringCoding.ZeroTerminated);
            light.Type = (LightType)stream.ReadByte();
            light.UnknownFloat1 = stream.ReadSingle();
            light.Position = new Vector4(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
            light.Rotation = new Vector4(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
            light.Range = stream.ReadSingle();
            light.InnerRange = stream.ReadSingle();

            light.Color = stream.ReadByteColor(ColorMemberOrder.AlphaFirst);

            light.UnknownByte1 = stream.Read1Byte();
            light.UnknownByte2 = stream.Read1Byte();
            light.UnknownByte3 = stream.Read1Byte();
            light.UnknownByte4 = stream.Read1Byte();
            light.UnknownByte5 = stream.Read1Byte();

            light.UnknownVector1 = new Vector4(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
            light.UnknownString1 = stream.ReadString(StringCoding.ZeroTerminated);
            light.ID = stream.ReadUInt32();

            return light;
        }

        public void WriteToStream(Stream stream)
        {
            stream.Write(Name, StringCoding.ZeroTerminated);
            stream.Write(ColorName, StringCoding.ZeroTerminated);
            stream.Write((byte)Type);
            stream.Write(UnknownFloat1);

            stream.Write(Position.x);
            stream.Write(Position.y);
            stream.Write(Position.z);
            stream.Write(Position.w);

            stream.Write(Rotation.x);
            stream.Write(Rotation.y);
            stream.Write(Rotation.z);
            stream.Write(Rotation.w);

            stream.Write(Range);
            stream.Write(InnerRange);

            stream.Write(Color, ColorMemberOrder.AlphaFirst);

            stream.Write(UnknownByte1);
            stream.Write(UnknownByte2);
            stream.Write(UnknownByte3);
            stream.Write(UnknownByte4);
            stream.Write(UnknownByte5);

            stream.Write(UnknownVector1.x);
            stream.Write(UnknownVector1.y);
            stream.Write(UnknownVector1.z);
            stream.Write(UnknownVector1.w);

            stream.Write(UnknownString1, StringCoding.ZeroTerminated);
            stream.Write(ID);
        }
    }
}
