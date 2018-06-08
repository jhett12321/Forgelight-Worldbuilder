namespace WorldBuilder.Terrain
{
    using System;
    using Formats.Cnk;
    using Meshes;
    using UnityEngine;

    public class ChunkMeshFactory
    {
        public Mesh CreateMeshFromData(string name, MeshData meshData)
        {
            // Create mesh, and apply the generated data.
            Mesh mesh = new Mesh();
            mesh.name = name;
            mesh.subMeshCount = 4;

            // Apply verts to mesh
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

        public MeshData GenerateMeshData(CnkLOD cnkData)
        {
            MeshData meshData = new MeshData();
            uint vertCount = 0;

            for (int i = 0; i < 4; i++)
            {
                CnkLOD.RenderBatch renderBatch = cnkData.RenderBatches[i];
                vertCount += renderBatch.VertexCount;
            }

            meshData.verts = new Vector3[vertCount];
            meshData.uvs = new Vector2[vertCount];

            // Verts and UVs
            uint offset = 0;
            for (int i = 0; i < 4; i++)
            {
                CnkLOD.RenderBatch renderBatch = cnkData.RenderBatches[i];
                CalculateVertsAndUVs(offset, cnkData, renderBatch, i, meshData.verts, meshData.uvs);
                offset += renderBatch.VertexCount;
            }

            meshData.triangles = new int[4][];

            // Triangles
            for (int i = 0; i < 4; i++)
            {
                CnkLOD.RenderBatch renderBatch = cnkData.RenderBatches[i];
                meshData.triangles[i] = GetRenderBatchTriangles(cnkData, renderBatch);
            }

            return meshData;
        }

        private void CalculateVertsAndUVs(uint offset, CnkLOD cnkData, CnkLOD.RenderBatch renderBatch, int index, Vector3[] verts, Vector2[] uvs)
        {
            for (int i = 0; i < renderBatch.VertexCount; i++)
            {
                int k = (int) (renderBatch.VertexOffset + i);
                double x = cnkData.Vertices[k].X + (index >> 1) * 64;
                double y = cnkData.Vertices[k].Y + (index % 2) * 64;
                double heightNear = (double) cnkData.Vertices[k].HeightNear / 64;

                verts[offset + i] = new Vector3((float)x, (float)heightNear, (float)y);
                uvs[offset + i] = new Vector2((float)y / 128, 1 - (float)x / 128);
            }
        }

        private int[] GetRenderBatchTriangles(CnkLOD cnkData, CnkLOD.RenderBatch renderBatch)
        {
            int[] indices = new int[renderBatch.IndexCount];

            for (int i = 0; i < renderBatch.IndexCount; i++)
            {
                indices[renderBatch.IndexCount - 1 - i] = Convert.ToInt32(cnkData.Indices[i + (int)renderBatch.IndexOffset] + renderBatch.VertexOffset);
            }

            return indices;
        }
    }
}