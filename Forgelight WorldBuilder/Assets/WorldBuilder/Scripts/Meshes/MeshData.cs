namespace WorldBuilder.Meshes
{
    using UnityEngine;
    using UnityEngine.Rendering;

    public struct MeshData
    {
        public Vector3[] verts;
        public Vector2[] uvs;
        public int[][]   triangles;

        public IndexFormat IndexFormat;
    }
}