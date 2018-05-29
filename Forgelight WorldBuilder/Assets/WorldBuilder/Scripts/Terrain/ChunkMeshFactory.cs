namespace WorldBuilder.Terrain
{
    using System;
    using System.Collections.Generic;
    using Formats.Cnk;
    using UnityEngine;

    public class ChunkMeshFactory
    {
        public Mesh CreateFromCnkLOD(CnkLOD cnkData)
        {
            Mesh mesh = new Mesh();
            mesh.name = cnkData.Name;
            mesh.subMeshCount = 4;

            List<Vector3> verts = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();

            // Verts and UVs
            for (int i = 0; i < 4; i++)
            {
                CnkLOD.RenderBatch renderBatch = cnkData.RenderBatches[i];
                CalculateVertsAndUVs(cnkData, renderBatch, i, verts, uvs);
            }

            // Apply verts to mesh
            mesh.SetVertices(verts);

            // Triangles
            for (int i = 0; i < 4; i++)
            {
                CnkLOD.RenderBatch renderBatch = cnkData.RenderBatches[i];
                int[] indices = GetRenderBatchTriangles(cnkData, renderBatch);
                mesh.SetTriangles(indices, i);
            }

            mesh.SetUVs(0, uvs);
            mesh.RecalculateNormals();
            mesh.UploadMeshData(true);

            return mesh;
        }

        private void CalculateVertsAndUVs(CnkLOD cnkData, CnkLOD.RenderBatch renderBatch, int index, List<Vector3> verts, List<Vector2> uvs)
        {
            for (uint i = 0; i < renderBatch.VertexCount; i++)
            {
                int k = (int) (renderBatch.VertexOffset + i);
                double x = cnkData.Vertices[k].X + (index >> 1) * 64;
                double y = cnkData.Vertices[k].Y + (index % 2) * 64;
                double heightNear = (double) cnkData.Vertices[k].HeightNear / 64;

                verts.Add(new Vector3((float)x, (float)heightNear, (float)y));
                uvs.Add(new Vector2((float)y / 128, 1 - (float)x / 128));
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