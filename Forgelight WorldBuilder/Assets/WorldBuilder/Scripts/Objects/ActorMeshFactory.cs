namespace WorldBuilder.Objects
{
    using System;
    using System.IO;
    using Formats.Dma;
    using Formats.Dme;
    using Materials;
    using Meshes;
    using UnityEngine;
    using UnityEngine.Assertions;
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

            int arrayPos = 0;
            foreach (Mesh mesh in modelData.Meshes)
            {
                ProcessMesh(arrayPos, modelData, mesh, meshData.verts, meshData.uvs);
                arrayPos += (int)mesh.VertexCount;
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

            mesh.indexFormat = meshData.verts.Length > 65535 ? IndexFormat.UInt32 : IndexFormat.UInt16;

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

        private void ProcessMesh(int arrayPos, Dme modelData, Mesh meshData, Vector3[] verts, Vector2[] uvs)
        {
            uint materialHash = modelData.Materials[(int)meshData.MaterialIndex].MaterialDefinitionHash;
            MaterialDefinition materialDefinition = materialDefinitionManager.GetMaterial(materialHash);

            uint vertexLayoutHash = materialDefinition.DrawStyles[DRAWSTYLE_INDEX].VertexLayoutNameHash;
            VertexLayout vertexLayout = materialDefinitionManager.GetVertexLayout(vertexLayoutHash);

            GetMeshVerts(verts, arrayPos, meshData, vertexLayout);
            GetMeshUVs(uvs, arrayPos, meshData, vertexLayout);
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
        /// Imports the mesh's texture coordinates.
        /// </summary>
        private void GetMeshUVs(Vector2[] uvs, int offset, Mesh meshData, VertexLayout vertexLayout)
        {
            VertexLayout.EntryInfo info;

            bool uvsExist = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Texcoord, 0, out info);

            if (uvsExist)
            {
                ReadVertexStream(uvs, offset, meshData, info);
            }
        }

        /// <summary>
        /// Imports the mesh's vertex data.
        /// </summary>
        private void GetMeshVerts(Vector3[] verts, int arrayPos, Mesh meshData, VertexLayout vertexLayout)
        {
            VertexLayout.EntryInfo info;

            bool vertsExist = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Position, 0, out info);

            Assert.IsTrue(vertsExist);

            ReadVertexStream(verts, arrayPos, meshData, info);
        }

        private void GetMeshNormals(Vector3[] normals, int arrayPos, Mesh meshData, VertexLayout vertexLayout)
        {
            VertexLayout.EntryInfo info;

            bool normalExists = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Normal, 0, out info);

            if (normalExists)
            {
                ReadVertexStream(normals, arrayPos, meshData, info);
            }
        }

        private void ReadVertexStream(Vector3[] targetArray, int arrayPos, Mesh meshData, VertexLayout.EntryInfo info)
        {
            Mesh.VertexStream stream = meshData.VertexStreams[info.streamIndex];

            for (int i = 0; i < meshData.VertexCount; i++)
            {
                targetArray[i + arrayPos] = ReadVector3(info.offset, stream, i, info.dataType);
            }
        }

        private void ReadVertexStream(Vector2[] targetArray, int arrayPos, Mesh meshData, VertexLayout.EntryInfo info)
        {
            Mesh.VertexStream stream = meshData.VertexStreams[info.streamIndex];

            for (int i = 0; i < meshData.VertexCount; i++)
            {
                targetArray[i + arrayPos] = ReadVector2(info.offset, stream, i, info.dataType);
            }
        }

        private Vector3 ReadVector3(int offset, Mesh.VertexStream vertexStream, int index, VertexLayout.Entry.DataTypes dataType)
        {
            Vector3 vector3 = new Vector3();

            switch (dataType)
            {
                case VertexLayout.Entry.DataTypes.Float3:
                    vector3.x = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 0);
                    vector3.y = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 4);
                    vector3.z = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 8);
                    break;
                default:
                    throw new InvalidDataException($"Unhandled stream DataType \"{dataType}\"");
            }

            return vector3;
        }

        private Vector2 ReadVector2(int offset, Mesh.VertexStream vertexStream, int index, VertexLayout.Entry.DataTypes dataType)
        {
            Vector2 vector2 = new Vector2();

            switch (dataType)
            {
                case VertexLayout.Entry.DataTypes.Float2:
                    vector2.x = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 0);
                    vector2.y = 1.0f - BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 4);
                    break;
                case VertexLayout.Entry.DataTypes.float16_2:
                    vector2.x = Mathf.HalfToFloat(BitConverter.ToUInt16(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 0));
                    vector2.y = 1.0f - Mathf.HalfToFloat(BitConverter.ToUInt16(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 2));
                    break;
                default:
                    throw new InvalidDataException($"Unhandled stream DataType \"{dataType}\"");
            }

            return vector2;
        }
    }
}