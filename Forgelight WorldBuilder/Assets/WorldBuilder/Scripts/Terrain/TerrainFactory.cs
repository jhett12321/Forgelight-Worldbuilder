namespace WorldBuilder.Terrain
{
    using System;
    using System.IO;
    using Formats.Cnk;
    using Formats.Pack;
    using Materials;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Zenject;
    using Object = UnityEngine.Object;

    public class TerrainFactory
    {
        private const int CHUNK_POS_OFFSET = 32;

        [Inject] private AssetManager assetManager;
        [Inject] private ChunkMeshFactory chunkMeshFactory;
        [Inject] private ChunkMaterialFactory materialFactory;

        private GameObject terrainParent;

        [Inject]
        public TerrainFactory(GameManager gameManager)
        {
            gameManager.OnGameLoaded += OnGameLoaded;
        }

        private void OnGameLoaded()
        {
            if (terrainParent != null)
            {
                Object.Destroy(terrainParent);
            }

            terrainParent = new GameObject("Forgelight Terrain");
        }

        public async void LoadZoneTerrain(string zone)
        {
            AssetRef[] assets = assetManager.GetAssetsByType(AssetType.CNK1, zone);

            if (assets == null || assets.Length == 0)
            {
                Debug.LogWarningFormat("Could not locate any chunk data associated with zone \"{0}\". Skipping load of terrain.", zone);
                return;
            }

            terrainParent.name = zone + " Terrain";

            foreach (AssetRef assetRef in assets)
            {
                CreateChunk(assetRef);
                await new WaitForEndOfFrame();
            }

            terrainParent.transform.localScale = new Vector3(2, 2, 2);
        }

        private void CreateChunk(AssetRef assetRef)
        {
            string chunkName = Path.GetFileNameWithoutExtension(assetRef.Name);

            // Mesh
            CnkLOD chunkData = assetManager.CreateAsset<CnkLOD>(assetRef);
            Assert.IsNotNull(chunkData);

            // Deserialization
            Mesh mesh;
            Material[] materials;

            try
            {
                mesh = chunkMeshFactory.CreateFromCnkLOD(chunkData);
                materials = materialFactory.GetMaterials(chunkData);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("An error occurred while creating chunk \"{0}\". See below for details.", assetRef.Name);
                Debug.LogException(e);
                return;
            }

            Assert.IsNotNull(mesh);
            Assert.IsNotNull(materials);
            Assert.IsFalse(materials.Length == 0);

            // GameObject Instance
            GameObject instance = new GameObject(chunkName);
            ForgelightChunk chunk = instance.AddComponent<ForgelightChunk>();
            MeshFilter meshFilter = instance.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = instance.AddComponent<MeshRenderer>();

            // Assign deserialized data
            meshFilter.sharedMesh = mesh;
            meshRenderer.sharedMaterials = materials;

            chunk.transform.SetParent(terrainParent.transform);

            // Position the terrain instance
            string[] nameElements = chunkName.Split('_');

            // Multiply the position on each axis by the size of the chunk, as we are given only chunk coordinates.
            int chunkPosX = (Convert.ToInt32(nameElements[2]) * CHUNK_POS_OFFSET);
            int chunkPosZ = (Convert.ToInt32(nameElements[1]) * CHUNK_POS_OFFSET);

            chunk.transform.position = new Vector3(chunkPosX, 0, chunkPosZ);
        }
    }
}