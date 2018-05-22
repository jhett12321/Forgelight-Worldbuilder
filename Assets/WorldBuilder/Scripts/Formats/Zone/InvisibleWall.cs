namespace WorldBuilder.Formats.Zone
{
    using System.IO;
    using Syroot.BinaryData;

    public class InvisibleWall
    {
        #region Structure
        public uint UnknownUInt32 { get; private set; }
        public float UnknownFloat1 { get; private set; }
        public float UnknownFloat2 { get; private set; }
        public float UnknownFloat3 { get; private set; }
        #endregion

        public static InvisibleWall ReadFromStream(Stream stream)
        {
            InvisibleWall invisibleWall = new InvisibleWall();

            invisibleWall.UnknownUInt32 = stream.ReadUInt32();
            invisibleWall.UnknownFloat1 = stream.ReadSingle();
            invisibleWall.UnknownFloat2 = stream.ReadSingle();
            invisibleWall.UnknownFloat3 = stream.ReadSingle();

            return invisibleWall;
        }

        public void WriteToStream(Stream stream)
        {
            stream.Write(UnknownUInt32);
            stream.Write(UnknownFloat1);
            stream.Write(UnknownFloat2);
            stream.Write(UnknownFloat3);
        }
    }
}
