namespace WorldBuilder.Formats.Zone
{
    using System.Collections.Generic;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;
    using Utils;

    public class Object
    {
        #region Structure
        public string ActorDefinition { get; set; }
        public float RenderDistance { get; set; }
        public List<Instance> Instances { get; set; }
        public class Instance
        {
            public Vector4 Position { get; set; }
            public Vector4 Rotation { get; set; }
            public Vector4 Scale { get; set; }
            public uint ID { get; set; }
            public bool DontCastShadows { get; set; }
            public float LODMultiplier { get; set; }

            public uint UnknownDword1 { get; set; }
            public uint UnknownDword2 { get; set; }
            public uint UnknownDword3 { get; set; }
            public uint UnknownDword4 { get; set; }
            public uint UnknownDword5 { get; set; }

        }
        #endregion

        public static Object ReadFromStream(Stream stream, ZoneType zoneType)
        {
            Object obj = new Object();

            obj.ActorDefinition = stream.ReadString(StringCoding.ZeroTerminated);
            obj.RenderDistance = stream.ReadSingle();

            obj.Instances = new List<Instance>();
            uint instancesLength = stream.ReadUInt32();

            for (uint i = 0; i < instancesLength; i++)
            {
                Instance instance = new Instance();

                instance.Position = stream.ReadVector4();
                instance.Rotation = stream.ReadVector4();
                instance.Scale = stream.ReadVector4();
                instance.ID = stream.ReadUInt32();
                instance.DontCastShadows = stream.ReadBoolean();
                instance.LODMultiplier = stream.ReadSingle();

                if (zoneType == ZoneType.H1Z1)
                {
                    instance.UnknownDword1 = stream.ReadUInt32();
                    instance.UnknownDword2 = stream.ReadUInt32();
                    instance.UnknownDword3 = stream.ReadUInt32();
                    instance.UnknownDword4 = stream.ReadUInt32();
                    instance.UnknownDword5 = stream.ReadUInt32();
                }

                obj.Instances.Add(instance);
            }

            return obj;
        }

        public void WriteToStream(Stream stream, ZoneType zoneType)
        {
            stream.Write(ActorDefinition, StringCoding.ZeroTerminated);
            stream.Write(RenderDistance);

            stream.Write((uint) Instances.Count);

            foreach (Instance instance in Instances)
            {
                stream.Write(instance.Position);
                stream.Write(instance.Rotation);
                stream.Write(instance.Scale);

                stream.Write(instance.ID);
                stream.Write(instance.DontCastShadows);
                stream.Write(instance.LODMultiplier);

                if (zoneType != ZoneType.H1Z1)
                {
                    continue;
                }

                stream.Write(instance.UnknownDword1);
                stream.Write(instance.UnknownDword2);
                stream.Write(instance.UnknownDword3);
                stream.Write(instance.UnknownDword4);
                stream.Write(instance.UnknownDword5);
            }
        }
    }
}
