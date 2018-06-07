namespace WorldBuilder.Terrain
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Formats.Cnk;
    using Formats.Pack;
    using Materials;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Zenject;

    public class TerrainFactory
    {
        private const int CHUNK_POS_OFFSET = 32;

        [Inject] private AssetManager assetManager;
        [Inject] private ChunkMeshFactory chunkMeshFactory;
        [Inject] private ChunkMaterialFactory materialFactory;

        public async Task<ForgelightChunk> CreateChunk(AssetRef assetRef)
        {
            await new WaitForBackgroundThread();
            string chunkName = Path.GetFileNameWithoutExtension(assetRef.Name);

            // Mesh
            CnkLOD chunkData = assetManager.CreateAsset<CnkLOD>(assetRef);
            Assert.IsNotNull(chunkData);

            // Deserialization
            await new WaitForUpdate();
            Mesh mesh;
            Material material;

            try
            {
                mesh = await chunkMeshFactory.CreateFromCnkLOD(chunkData);
                material = materialFactory.GetMaterial(chunkData);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("An error occurred while creating chunk \"{0}\". See below for details.", assetRef.Name);
                Debug.LogException(e);
                assetManager.Dispose(chunkData);
                return null;
            }

            assetManager.Dispose(chunkData);
            Assert.IsNotNull(mesh);
            Assert.IsNotNull(material);

            // GameObject Instance
            GameObject instance = new GameObject(chunkName);
            ForgelightChunk chunk = instance.AddComponent<ForgelightChunk>();
            MeshFilter meshFilter = instance.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = instance.AddComponent<MeshRenderer>();

            // Assign deserialized data
            meshFilter.sharedMesh = mesh;

            // TODO Make a combined mesh instead of supplying the same material 4 times.
            Material[] materials = new Material[4];
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = material;
            }

            meshRenderer.sharedMaterials = materials;

            // Position the terrain instance
            string[] nameElements = chunkName.Split('_');

            // Multiply the position on each axis by the size of the chunk, as we are given only chunk coordinates.
            int chunkPosX = (Convert.ToInt32(nameElements[2]) * CHUNK_POS_OFFSET);
            int chunkPosZ = (Convert.ToInt32(nameElements[1]) * CHUNK_POS_OFFSET);

            chunk.transform.position = new Vector3(chunkPosX, 0, chunkPosZ);

            return chunk;
        }
    }
}