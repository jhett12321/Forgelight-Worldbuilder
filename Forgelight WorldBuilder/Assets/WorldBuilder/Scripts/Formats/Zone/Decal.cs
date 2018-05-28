namespace WorldBuilder.Formats.Zone
{
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;
    using Utils;

    public class Decal
    {
        #region Structure
        public float UnknownFloat1 { get; private set; }
        public Vector4 Position { get; private set; }
        public float UnknownFloat2 { get; private set; }
        public float UnknownFloat3 { get; private set; }
        public float UnknownFloat4 { get; private set; }
        public float UnknownFloat5 { get; private set; }
        public uint DecimalDigits6And4 { get; private set; } //Shaql: I mean, uh, the last 4 digits in decimal seem to be similar or same for several values, thus probably have some significance
        public string Name { get; private set; }
        public float UnknownFloat6 { get; private set; }
        public float UnknownFloat7 { get; private set; }
        public float UnknownFloat8 { get; private set; }
        public uint UnknownUInt { get; private set; }
        #endregion

        public static Decal ReadFromStream(Stream stream)
        {
            Decal decal = new Decal();

            decal.UnknownFloat1 = stream.ReadSingle();
            decal.Position = stream.ReadVector4();
            decal.UnknownFloat2 = stream.ReadSingle();
            decal.UnknownFloat3 = stream.ReadSingle();
            decal.UnknownFloat4 = stream.ReadSingle();
            decal.UnknownFloat5 = stream.ReadSingle();
            decal.DecimalDigits6And4 = stream.ReadUInt32();
            decal.Name = stream.ReadString(StringCoding.ZeroTerminated);
            decal.UnknownFloat6 = stream.ReadSingle();
            decal.UnknownFloat7 = stream.ReadSingle();
            decal.UnknownFloat8 = stream.ReadSingle();
            decal.UnknownUInt = stream.ReadUInt32();

            return decal;
        }

        public void WriteToStream(Stream stream)
        {
            stream.Write(UnknownFloat1);
            stream.Write(Position);
            stream.Write(UnknownFloat2);
            stream.Write(UnknownFloat3);
            stream.Write(UnknownFloat4);
            stream.Write(UnknownFloat5);
            stream.Write(DecimalDigits6And4);
            stream.Write(Name, StringCoding.ZeroTerminated);
            stream.Write(UnknownFloat6);
            stream.Write(UnknownFloat7);
            stream.Write(UnknownFloat8);
            stream.Write(UnknownUInt);
        }
    }
}
