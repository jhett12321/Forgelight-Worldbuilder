namespace WorldBuilder.Formats.Zone
{
    using System.IO;
    using Syroot.BinaryData;

    public class Flora
    {
        #region Structure
        public string Name { get; private set; }
        public string Texture { get; private set; }
        public string Model { get; private set; }
        public bool UnknownBoolean1 { get; private set; }
        public float UnknownFloat1 { get; private set; }
        public float UnknownFloat2 { get; private set; }
        public float UnknownFloat3 { get; private set; }
        public float UnknownFloat4 { get; private set; }
        public float UnknownFloat5 { get; private set; }

        #endregion

        public static Flora ReadFromStream(Stream stream, ZoneType zoneType)
        {
            Flora flora = new Flora();

            flora.Name = stream.ReadString(StringCoding.ZeroTerminated);
            flora.Texture = stream.ReadString(StringCoding.ZeroTerminated);
            flora.Model = stream.ReadString(StringCoding.ZeroTerminated);
            flora.UnknownBoolean1 = stream.ReadBoolean();
            flora.UnknownFloat1 = stream.ReadSingle();
            flora.UnknownFloat2 = stream.ReadSingle();

            if (zoneType == ZoneType.H1Z1)
            {
                flora.UnknownFloat3 = stream.ReadSingle();
                flora.UnknownFloat4 = stream.ReadSingle();
                flora.UnknownFloat5 = stream.ReadSingle();
            }

            return flora;
        }

        public void WriteToStream(Stream stream, ZoneType zoneType)
        {
            stream.Write(Name, StringCoding.ZeroTerminated);
            stream.Write(Texture, StringCoding.ZeroTerminated);
            stream.Write(Model, StringCoding.ZeroTerminated);

            stream.Write(UnknownBoolean1);
            stream.Write(UnknownFloat1);
            stream.Write(UnknownFloat2);

            if (zoneType == ZoneType.H1Z1)
            {
                stream.Write(UnknownFloat3);
                stream.Write(UnknownFloat4);
                stream.Write(UnknownFloat5);
            }
        }
    }
}
