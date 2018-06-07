namespace WorldBuilder.Formats.Zone
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;
    using UnityEngine.Assertions;

    public class Zone : IReadableAsset, IWritableAsset
    {
        public ByteConverter ByteConverter => ByteConverter.Little;
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ZoneType ZoneType { get; private set; }

        #region Structure
        // Header
        private const string MAGIC = "ZONE";

        public uint Version { get; private set; }
        public Dictionary<string, uint> Offsets { get; private set; }
        public uint QuadsPerTile { get; private set; }
        public float TileSize { get; private set; }
        public float TileHeight { get; private set; }
        public uint VertsPerTile { get; private set; }
        public uint TilesPerChunk { get; private set; }
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public uint ChunksX { get; private set; }
        public uint ChunksY { get; private set; }

        // Data
        public List<Eco> Ecos { get; private set; }
        public List<Flora> Floras { get; private set; }
        public List<InvisibleWall> InvisibleWalls { get; private set; }
        public List<Object> Objects { get; private set; }
        public List<Light> Lights { get; private set; }
        public List<Unknown> Unknowns { get; private set; }
        public List<Decal> Decals { get; private set; }

        #endregion

        public bool Deserialize(BinaryStream stream)
        {
            // Header
            string magic = stream.ReadString(4);
            Assert.AreEqual(MAGIC, magic, "Zone file does not have the correct header!");

            Version = stream.ReadUInt32();

            if (!Enum.IsDefined(typeof(ZoneType), (int)Version))
            {
                Debug.LogWarning("Could not decode zone " + Name + ". Unknown ZONE version " + Version);
                return false;
            }

            ZoneType = (ZoneType) (int) Version;

            if (ZoneType == ZoneType.H1Z1)
            {
                stream.ReadUInt32();
            }

            Offsets = new Dictionary<string, uint>();
            Offsets["ecos"] = stream.ReadUInt32();
            Offsets["floras"] = stream.ReadUInt32();
            Offsets["invisibleWalls"] = stream.ReadUInt32();
            Offsets["objects"] = stream.ReadUInt32();
            Offsets["lights"] = stream.ReadUInt32();
            Offsets["unknowns"] = stream.ReadUInt32();

            if (ZoneType == ZoneType.H1Z1)
            {
                Offsets["decals"] = stream.ReadUInt32();
            }

            QuadsPerTile = stream.ReadUInt32();
            TileSize = stream.ReadSingle();
            TileHeight = stream.ReadSingle();
            VertsPerTile = stream.ReadUInt32();
            TilesPerChunk = stream.ReadUInt32();
            StartX = stream.ReadInt32();
            StartY = stream.ReadInt32();
            ChunksX = stream.ReadUInt32();
            ChunksY = stream.ReadUInt32();

            // Ecos
            Ecos = new List<Eco>();
            uint ecosLength = stream.ReadUInt32();

            for (uint i = 0; i < ecosLength; i++)
            {
                Ecos.Add(Eco.ReadFromStream(stream));
            }

            // Floras
            Floras = new List<Flora>();
            uint florasLength = stream.ReadUInt32();

            for (uint i = 0; i < florasLength; i++)
            {
                Floras.Add(Flora.ReadFromStream(stream, ZoneType));
            }

            // Invisible Walls
            InvisibleWalls = new List<InvisibleWall>();
            uint invisibleWallsLength = stream.ReadUInt32();

            for (uint i = 0; i < invisibleWallsLength; i++)
            {
                InvisibleWalls.Add(InvisibleWall.ReadFromStream(stream));
            }

            // Objects
            Objects = new List<Object>();
            uint objectsLength = stream.ReadUInt32();

            for (uint i = 0; i < objectsLength; i++)
            {
                Objects.Add(Object.ReadFromStream(stream, ZoneType));
            }

            // Lights
            Lights = new List<Light>();
            uint lightsLength = stream.ReadUInt32();

            for (uint i = 0; i < lightsLength; i++)
            {
                Lights.Add(Light.ReadFromStream(stream));
            }

            // Unknowns
            uint unknownsLength = stream.ReadUInt32();
            Unknowns = new List<Unknown>((int) unknownsLength);

            //for (int i = 0; i < unknownsLength; i++)
            //{
            //    //Unknowns.Add(Unknown.ReadFromStream(stream));
            //    //???
            //}

            // Decals
            if (ZoneType == ZoneType.H1Z1)
            {
                uint decalsLength = stream.ReadUInt32();
                Decals = new List<Decal>((int) decalsLength);

                for (int i = 0; i < decalsLength; i++)
                {
                    Decals.Add(Decal.ReadFromStream(stream));
                }
            }

            return true;
        }

        public bool Serialize(Stream stream)
        {
            if (!stream.CanWrite || !stream.CanSeek)
            {
                return false;
            }

            // Header
            stream.Write(MAGIC, StringCoding.Raw);
            stream.Write(Version);

            // Offsets
            long offsetsPosition = stream.Position;

            // Allocates space for the offset locations.
            stream.Write(0u);
            stream.Write(0u);
            stream.Write(0u);
            stream.Write(0u);
            stream.Write(0u);
            stream.Write(0u);

            // stream.Write(Offsets["ecos"]);
            // stream.Write(Offsets["floras"]);
            // stream.Write(Offsets["invisibleWalls"]);
            // stream.Write(Offsets["objects"]);
            // stream.Write(Offsets["lights"]);
            // stream.Write(Offsets["unknowns"]);

            Dictionary<string, uint> offsets = new Dictionary<string, uint>();

            // Misc Header
            stream.Write(QuadsPerTile);
            stream.Write(TileSize);
            stream.Write(TileHeight);
            stream.Write(VertsPerTile);
            stream.Write(TilesPerChunk);
            stream.Write(StartX);
            stream.Write(StartY);
            stream.Write(ChunksX);
            stream.Write(ChunksY);

            // Ecos
            offsets["ecos"] = (uint) stream.Position;
            stream.Write((uint) Ecos.Count);

            foreach (Eco eco in Ecos)
            {
                eco.WriteToStream(stream);
            }

            // Floras
            offsets["floras"] = (uint) stream.Position;
            stream.Write((uint) Floras.Count);

            foreach (Flora flora in Floras)
            {
                flora.WriteToStream(stream, ZoneType);
            }

            offsets["invisibleWalls"] = (uint) stream.Position;
            stream.Write((uint) InvisibleWalls.Count);

            foreach (InvisibleWall invisibleWall in InvisibleWalls)
            {
                invisibleWall.WriteToStream(stream);
            }

            offsets["objects"] = (uint) stream.Position;
            stream.Write((uint) Objects.Count);

            foreach (Object obj in Objects)
            {
                obj.WriteToStream(stream, ZoneType);
            }

            offsets["lights"] = (uint) stream.Position;
            stream.Write((uint) Lights.Count);

            foreach (Light light in Lights)
            {
                light.WriteToStream(stream);
            }

            offsets["unknowns"] = (uint) stream.Position;
            stream.Write((uint) Unknowns.Count);

            //???
            //foreach (Unknown unknown in Unknowns)
            //{
            //}

            if (ZoneType == ZoneType.H1Z1)
            {
                offsets["decals"] = (uint)stream.Position;
                stream.Write((uint) Decals.Count);

                foreach (Decal decal in Decals)
                {
                    decal.WriteToStream(stream);
                }
            }

            //Update offset values.
            stream.Seek(offsetsPosition, SeekOrigin.Begin);
            stream.Write(offsets["ecos"]);
            stream.Write(offsets["floras"]);
            stream.Write(offsets["invisibleWalls"]);
            stream.Write(offsets["objects"]);
            stream.Write(offsets["lights"]);
            stream.Write(offsets["unknowns"]);

            if (ZoneType == ZoneType.H1Z1)
            {
                stream.Write(offsets["decals"]);
            }

            return true;
        }
    }
}
