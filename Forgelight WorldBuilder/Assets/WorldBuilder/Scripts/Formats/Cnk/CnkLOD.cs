namespace WorldBuilder.Formats.Cnk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LzhamWrapper;
    using Syroot.BinaryData;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Utils;
    using Utils.Pools;

    public class CnkLOD : IReadableAsset, IPoolResetable
    {
        public ByteConverter ByteConverter => ByteConverter.Little;
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ChunkType ChunkType { get; private set; }

        #region Structure
        //Header
        private const string MAGIC = "CNK";
        public uint Version { get; private set; }

        public uint DecompressedSize { get; private set; }
        public uint CompressedSize { get; private set; }

        // Buffers
        public Buffer<byte> CompressedBuffer = new Buffer<byte>();
        public Buffer<byte>  DecompressedBuffer = new Buffer<byte>();

        //Textures
        public Buffer<Texture> Textures = new Buffer<Texture>();
        public class Texture
        {
            public readonly Buffer<byte> ColorNXMap = new Buffer<byte>();
            public readonly Buffer<byte> SpecNyMap = new Buffer<byte>();
            public readonly Buffer<byte> ExtraData1 = new Buffer<byte>();
            public readonly Buffer<byte> ExtraData2 = new Buffer<byte>();
            public readonly Buffer<byte> ExtraData3 = new Buffer<byte>();
            public readonly Buffer<byte> ExtraData4 = new Buffer<byte>();
        }

        //Verts per side
        public uint VertsPerSide { get; private set; }

        public Dictionary<int, Dictionary<int, HeightMap>> HeightMaps = new Dictionary<int, Dictionary<int, HeightMap>>();
        public class HeightMap
        {
            public short Val1 { get; set; }
            public byte Val2 { get; set; }
            public byte Val3 { get; set; }
        }

        //Indices
        public List<ushort> Indices { get; private set; }

        //Verts
        public List<Vertex> Vertices { get; private set; }
        public class Vertex
        {
            public short X { get; set; }
            public short Y { get; set; }
            public short HeightFar { get; set; }
            public short HeightNear { get; set; }
            public uint Color { get; set; }
        }

        //Render Batches
        public List<RenderBatch> RenderBatches { get; private set; }
        public class RenderBatch
        {
            public uint Unknown { get; set; }
            public uint IndexOffset { get; set; }
            public uint IndexCount { get; set; }
            public uint VertexOffset { get; set; }
            public uint VertexCount { get; set; }
        }

        //Optimized Draw
        public List<OptimizedDraw> OptimizedDraws { get; private set; }
        public class OptimizedDraw
        {
            public List<byte> Data { get; set; }
        }

        //Unknown Data
        public List<ushort> UnknownShorts1 { get; private set; }

        //Unknown Data
        public List<Vector3> UnknownVectors1 { get; private set; }

        //Tile Occluder Info
        public List<TileOccluderInfo> TileOccluderInfos { get; private set; }
        public class TileOccluderInfo
        {
            public List<byte> Data { get; set; }
        }
        #endregion

        public CnkLOD()
        {
            Indices = new List<ushort>();
            Vertices = new List<Vertex>();
            RenderBatches = new List<RenderBatch>();
            OptimizedDraws = new List<OptimizedDraw>();
            UnknownShorts1 = new List<ushort>();
            UnknownVectors1 = new List<Vector3>();
            TileOccluderInfos = new List<TileOccluderInfo>();
        }

        public void Reset()
        {
            Indices.Clear();
            Vertices.Clear();
            RenderBatches.Clear();
            OptimizedDraws.Clear();
            UnknownShorts1.Clear();
            UnknownVectors1.Clear();
            TileOccluderInfos.Clear();
        }

        public bool Deserialize(BinaryStream stream)
        {
            // Header
            string magic = stream.ReadString(3);

            Assert.AreEqual(MAGIC, magic, "Chunk header mismatch!");

            int lodLevel = Convert.ToInt32(stream.ReadString(1));

            Assert.AreNotEqual(0, lodLevel, "Use the CNK0 serializer for LOD0 terrain chunks.");

            Version = stream.ReadUInt32();

            if (!Enum.IsDefined(typeof(ChunkType), (int) Version))
            {
                Debug.LogWarning("Could not decode chunk " + Name + ". Unknown cnk version " + Version);
                return false;
            }

            ChunkType = (ChunkType) Version;

            DecompressedSize = stream.ReadUInt32();
            CompressedSize   = stream.ReadUInt32();

            // Read the compressed buffer.
            stream.ReadBytes(CompressedBuffer, (int)CompressedSize);

            // Decompression
            // Make sure our buffer is large enough.
            DecompressedBuffer.PrepareBuffer((int) DecompressedSize);

            // Perform decompression using Lzham.
            InflateReturnCode result = LzhamInterop.DecompressForgelightData(CompressedBuffer.Data, CompressedSize, DecompressedBuffer.Data, DecompressedSize);

            if (result != InflateReturnCode.LZHAM_Z_STREAM_END && result != InflateReturnCode.LZHAM_Z_OK)
            {
                //This chunk is invalid, or something went wrong.
                return false;
            }

            using (MemoryStream decompressedStream = new MemoryStream(DecompressedBuffer.Data, 0, (int) DecompressedSize))
            {
                //Textures
                uint textureCount = decompressedStream.ReadUInt32();
                Textures.PrepareBuffer((int)textureCount);

                for (int i = 0; i < textureCount; i++)
                {
                    Texture texture = Textures[i];

                    if (texture == null)
                    {
                        texture = new Texture();
                        Textures[i] = texture;
                    }

                    uint colorNxMapSize = decompressedStream.ReadUInt32();
                    decompressedStream.ReadBytes(texture.ColorNXMap, (int)colorNxMapSize);

                    uint specNyMapSize = decompressedStream.ReadUInt32();
                    decompressedStream.ReadBytes(texture.SpecNyMap, (int)specNyMapSize);

                    uint extraData1Size = decompressedStream.ReadUInt32();
                    decompressedStream.ReadBytes(texture.ExtraData1, (int)extraData1Size);

                    uint extraData2Size = decompressedStream.ReadUInt32();
                    decompressedStream.ReadBytes(texture.ExtraData2, (int)extraData2Size);

                    uint extraData3Size = decompressedStream.ReadUInt32();
                    decompressedStream.ReadBytes(texture.ExtraData3, (int)extraData3Size);

                    uint extraData4Size = decompressedStream.ReadUInt32();
                    decompressedStream.ReadBytes(texture.ExtraData4, (int)extraData4Size);
                }

                //Verts Per Side
                VertsPerSide = decompressedStream.ReadUInt32();

                //Height Maps
                uint heightMapCount = decompressedStream.ReadUInt32();

                int n = (int) (heightMapCount / 4);

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Dictionary<int, HeightMap> entry;

                        if (!HeightMaps.ContainsKey(i))
                        {
                            entry = new Dictionary<int, HeightMap>();
                            HeightMaps[i] = entry;
                        }

                        else
                        {
                            entry = HeightMaps[i];
                        }

                        HeightMap heightMapData = new HeightMap();
                        heightMapData.Val1 = decompressedStream.ReadInt16();
                        heightMapData.Val2 = decompressedStream.Read1Byte();
                        heightMapData.Val3 = decompressedStream.Read1Byte();

                        entry[j] = heightMapData;
                    }
                }

                //Indices
                uint indexCount = decompressedStream.ReadUInt32();

                for (int i = 0; i < indexCount; i++)
                {
                    Indices.Add(decompressedStream.ReadUInt16());
                }

                //Verts
                uint vertCount = decompressedStream.ReadUInt32();

                for (int i = 0; i < vertCount; i++)
                {
                    Vertex vertex = new Vertex();

                    vertex.X = decompressedStream.ReadInt16();
                    vertex.Y = decompressedStream.ReadInt16();
                    vertex.HeightFar = decompressedStream.ReadInt16();
                    vertex.HeightNear = decompressedStream.ReadInt16();
                    vertex.Color = decompressedStream.ReadUInt32();

                    Vertices.Add(vertex);
                }

                //TODO HACK - Daybreak, why are some chunks (that have a version 2 header) actually version 1?
                long offset = decompressedStream.Position;
                try
                {
                    //Render Batches
                    uint renderBatchCount = decompressedStream.ReadUInt32();

                    for (int i = 0; i < renderBatchCount; i++)
                    {
                        RenderBatch renderBatch = new RenderBatch();

                        if (ChunkType == ChunkType.H1Z1_Planetside2V2)
                        {
                            renderBatch.Unknown = decompressedStream.ReadUInt32();
                        }

                        renderBatch.IndexOffset = decompressedStream.ReadUInt32();
                        renderBatch.IndexCount = decompressedStream.ReadUInt32();
                        renderBatch.VertexOffset = decompressedStream.ReadUInt32();
                        renderBatch.VertexCount = decompressedStream.ReadUInt32();

                        RenderBatches.Add(renderBatch);
                    }

                    //Optimized Draw
                    uint optimizedDrawCount = decompressedStream.ReadUInt32();

                    for (int i = 0; i < optimizedDrawCount; i++)
                    {
                        OptimizedDraw optimizedDraw = new OptimizedDraw();
                        optimizedDraw.Data = decompressedStream.ReadBytes(320).ToList();

                        OptimizedDraws.Add(optimizedDraw);
                    }

                    //Unknown Data
                    uint unknownShort1Count = decompressedStream.ReadUInt32();

                    for (int i = 0; i < unknownShort1Count; i++)
                    {
                        UnknownShorts1.Add(decompressedStream.ReadUInt16());
                    }

                    //Unknown Data
                    uint unknownVectors1Count = decompressedStream.ReadUInt32();

                    for (int i = 0; i < unknownVectors1Count; i++)
                    {
                        UnknownVectors1.Add(new Vector3(decompressedStream.ReadSingle(), decompressedStream.ReadSingle(),
                            decompressedStream.ReadSingle()));
                    }

                    //Tile Occluder Info
                    uint tileOccluderCount = decompressedStream.ReadUInt32();

                    if (tileOccluderCount > 16)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    for (int i = 0; i < tileOccluderCount; i++)
                    {
                        TileOccluderInfo tileOccluderInfo = new TileOccluderInfo();
                        tileOccluderInfo.Data = decompressedStream.ReadBytes(64).ToList();

                        TileOccluderInfos.Add(tileOccluderInfo);
                    }
                }
                catch (Exception)
                {
                    // Some of these may have been populated from the "try".
                    RenderBatches.Clear();
                    OptimizedDraws.Clear();
                    UnknownShorts1.Clear();
                    UnknownVectors1.Clear();
                    TileOccluderInfos.Clear();

                    decompressedStream.Position = offset;

                    //Render Batches
                    uint renderBatchCount = decompressedStream.ReadUInt32();

                    for (int i = 0; i < renderBatchCount; i++)
                    {
                        RenderBatch renderBatch = new RenderBatch();

                        renderBatch.IndexOffset = decompressedStream.ReadUInt32();
                        renderBatch.IndexCount = decompressedStream.ReadUInt32();
                        renderBatch.VertexOffset = decompressedStream.ReadUInt32();
                        renderBatch.VertexCount = decompressedStream.ReadUInt32();

                        RenderBatches.Add(renderBatch);
                    }

                    //Optimized Draw
                    uint optimizedDrawCount = decompressedStream.ReadUInt32();

                    for (int i = 0; i < optimizedDrawCount; i++)
                    {
                        OptimizedDraw optimizedDraw = new OptimizedDraw();
                        optimizedDraw.Data = decompressedStream.ReadBytes(320).ToList();

                        OptimizedDraws.Add(optimizedDraw);
                    }

                    //Unknown Data
                    uint unknownShort1Count = decompressedStream.ReadUInt32();

                    for (int i = 0; i < unknownShort1Count; i++)
                    {
                        UnknownShorts1.Add(decompressedStream.ReadUInt16());
                    }

                    //Unknown Data
                    uint unknownVectors1Count = decompressedStream.ReadUInt32();

                    for (int i = 0; i < unknownVectors1Count; i++)
                    {
                        UnknownVectors1.Add(new Vector3(decompressedStream.ReadSingle(), decompressedStream.ReadSingle(),
                            decompressedStream.ReadSingle()));
                    }

                    //Tile Occluder Info
                    uint tileOccluderCount = decompressedStream.ReadUInt32();

                    if (tileOccluderCount > 16)
                    {
                        return false;
                    }

                    for (int i = 0; i < tileOccluderCount; i++)
                    {
                        TileOccluderInfo tileOccluderInfo = new TileOccluderInfo();
                        tileOccluderInfo.Data = decompressedStream.ReadBytes(64).ToList();

                        TileOccluderInfos.Add(tileOccluderInfo);
                    }
                }
            }

            return true;
        }
    }
}
