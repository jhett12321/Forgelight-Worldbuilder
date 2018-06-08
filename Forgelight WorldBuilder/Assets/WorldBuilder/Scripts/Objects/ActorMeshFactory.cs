namespace WorldBuilder.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Formats.Dma;
    using Formats.Dme;
    using Materials;
    using Meshes;
    using UnityEngine;
    using UnityEngine.Rendering;
    using Zenject;
    using Mesh = Formats.Dme.Mesh;

    /// <summary>
    /// Creates Unity-Compatible Meshes from Forgelight Model data.
    /// </summary>
    public class ActorMeshFactory
    {
        [Inject] private MaterialDefinitionManager materialDefinitionManager;

        // TODO MAGIC 0 DrawStyle index value.
        private const int DRAWSTYLE_INDEX = 0;

        public MeshData GenerateMeshData(Dme modelData)
        {
            MeshData meshData = new MeshData();

            uint totalVerts = 0;

            // Verts and UVs
            foreach (Mesh mesh in modelData.Meshes)
            {
                totalVerts += mesh.VertexCount;
            }

            meshData.verts = new Vector3[totalVerts];
            meshData.uvs = new Vector2[totalVerts];

            int offset = 0;
            foreach (Mesh mesh in modelData.Meshes)
            {
                ProcessMesh(offset, modelData, mesh, meshData.verts, meshData.uvs);
                offset += (int)mesh.VertexCount;
            }

            // Triangles
            meshData.triangles = new int[modelData.Meshes.Count][];
            uint vertCount = 0;
            for (int i = 0; i < modelData.Meshes.Count; i++)
            {
                Mesh mesh = modelData.Meshes[i];

                meshData.triangles[i] = GetMeshTriangles(mesh, vertCount);

                vertCount += mesh.VertexCount;
            }

            meshData.IndexFormat = GetIndexFormat(modelData.Meshes[0]);

            return meshData;
        }

        public UnityEngine.Mesh CreateMeshFromData(string name, MeshData meshData)
        {
            UnityEngine.Mesh mesh = new UnityEngine.Mesh();
            mesh.name = name;
            mesh.subMeshCount = meshData.triangles.Length;

            mesh.vertices = meshData.verts;
            mesh.uv = meshData.uvs;

            for (int i = 0; i < meshData.triangles.Length; i++)
            {
                mesh.SetTriangles(meshData.triangles[i], i);
            }

            mesh.RecalculateNormals();
            mesh.UploadMeshData(true);

            return mesh;
        }

        private void ProcessMesh(int offset, Dme modelData, Mesh meshData, Vector3[] verts, Vector2[] uvs)
        {
            uint materialHash = modelData.Materials[(int)meshData.MaterialIndex].MaterialDefinitionHash;
            MaterialDefinition materialDefinition = materialDefinitionManager.GetMaterial(materialHash);

            uint vertexLayoutHash = materialDefinition.DrawStyles[DRAWSTYLE_INDEX].VertexLayoutNameHash;
            VertexLayout vertexLayout = materialDefinitionManager.GetVertexLayout(vertexLayoutHash);

            GetMeshVerts(offset, verts, meshData, vertexLayout);
            GetMeshUVs(offset, uvs, meshData, vertexLayout);
        }

        private IndexFormat GetIndexFormat(Mesh meshData)
        {
            switch (meshData.IndexSize)
            {
                case 2:
                    return IndexFormat.UInt16;
                case 4:
                    return IndexFormat.UInt32;
                default:
                    throw new ArgumentOutOfRangeException("Unknown Index Format/Size: " + meshData.IndexSize);
            }
        }

        private int[] GetMeshTriangles(Mesh meshData, uint vertCount)
        {
            IndexFormat format = GetIndexFormat(meshData);

            int[] indices = new int[meshData.IndexCount];

            switch (format)
            {
                case IndexFormat.UInt16:
                    for (int i = 0; i < meshData.IndexCount; i++)
                    {
                        // We reverse the indices otherwise our normals will be inverted.
                        indices[meshData.IndexCount - 1 - i] = Convert.ToInt32(vertCount + BitConverter.ToUInt16(meshData.IndexData, i * 2));
                    }
                    break;
                case IndexFormat.UInt32:
                    for (int i = 0; i < meshData.IndexCount; i++)
                    {
                        // We reverse the indices otherwise our normals will be inverted.
                        indices[meshData.IndexCount - 1 - i] = Convert.ToInt32(vertCount + BitConverter.ToUInt32(meshData.IndexData, i * 4));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return indices;
        }

        /// <summary>
        /// Imports the mesh's texture coordinates into the working Unity mesh.
        /// </summary>
        /// <param name="meshInstance"></param>
        /// <param name="meshData"></param>
        private void GetMeshUVs(int offset, Vector2[] uvs, Mesh meshData, VertexLayout vertexLayout)
        {
            VertexLayout.Entry.DataTypes texCoord0DataType;
            int texCoord0Offset;
            int texCoord0StreamIndex;

            bool texCoord0Present = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Texcoord, 0, out texCoord0DataType, out texCoord0StreamIndex, out texCoord0Offset);

            if (!texCoord0Present)
            {
                return;
            }

            Mesh.VertexStream texCoord0Stream = meshData.VertexStreams[texCoord0StreamIndex];

            for (int i = 0; i < meshData.VertexCount; ++i)
            {
                Vector2 uv = new Vector2();

                switch (texCoord0DataType)
                {
                    case VertexLayout.Entry.DataTypes.Float2:
                    {
                        uv.x = BitConverter.ToSingle(texCoord0Stream.Data, (i * texCoord0Stream.BytesPerVertex) + 0);
                        uv.y = 1.0f - BitConverter.ToSingle(texCoord0Stream.Data, (i * texCoord0Stream.BytesPerVertex) + 4);
                        break;
                    }
                    case VertexLayout.Entry.DataTypes.float16_2:
                    {
                        uv.x = Mathf.HalfToFloat(BitConverter.ToUInt16(texCoord0Stream.Data, (i * texCoord0Stream.BytesPerVertex) + texCoord0Offset + 0));
                        uv.y = 1.0f - Mathf.HalfToFloat(BitConverter.ToUInt16(texCoord0Stream.Data, (i * texCoord0Stream.BytesPerVertex) + texCoord0Offset + 2));
                        break;
                    }
                }

                uvs[offset + i] = uv;
            }
        }

        /// <summary>
        /// Imports the mesh's vertex data into the working Unity mesh.
        /// </summary>
        /// <param name="meshInstance"></param>
        /// <param name="meshData"></param>
        /// <param name="vertexLayout"></param>
        private void GetMeshVerts(int offset, Vector3[] verts, Mesh meshData, VertexLayout vertexLayout)
        {
            // Geometric Verts
            VertexLayout.Entry.DataTypes positionDataType;
            int positionOffset;
            int positionStreamIndex;

            vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStreamIndex, out positionOffset);

            Mesh.VertexStream positionStream = meshData.VertexStreams[positionStreamIndex];

            for (int i = 0; i < meshData.VertexCount; ++i)
            {
                verts[offset + i] = ReadVector3(positionOffset, positionStream, i);
            }
        }

        private Vector3 ReadVector3(int offset, Mesh.VertexStream vertexStream, int index)
        {
            Vector3 vector3 = new Vector3();

            vector3.x = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex*index) + offset + 0);
            vector3.y = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex*index) + offset + 4);
            vector3.z = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex*index) + offset + 8);

            return vector3;
        }
    }
}