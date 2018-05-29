namespace WorldBuilder.Materials
{
    using Formats.Dma;
    using Formats.Dme;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Zenject;
    using Material = UnityEngine.Material;
    using Mesh = Formats.Dme.Mesh;

    /// <summary>
    /// Creates Unity-Compatible Materials for Actor Objects.
    /// </summary>
    public class ActorMaterialFactory
    {
        [Inject] private AssetManager assetManager;
        // TODO Use Material Definition Manager to map material definitions to recreated unity shaders.
        [Inject] private MaterialDefinitionManager materialDefinitionManager;

        private Material source;

        public ActorMaterialFactory(Material source)
        {
            this.source = source;
        }

        public Material[] GetActorMaterials(Dme model)
        {
            Material[] materials = new Material[model.Meshes.Count];

            for (int i = 0; i < model.Meshes.Count; i++)
            {
                materials[i] = CreateMeshMaterial(model.Meshes[i]);
            }

            return materials;
        }

        private Material CreateMeshMaterial(Mesh mesh)
        {
            Texture2D diffuseTex = CreateTexture(mesh.BaseDiffuse);
            Texture2D packedSpecTex = CreateTexture(mesh.SpecMap);
            Texture2D normalTex = CreateTexture(mesh.BumpMap);

            Material material = new Material(source);

            if (diffuseTex != null)
            {
                material.SetTexture("_MainTex", diffuseTex);
            }
            if (packedSpecTex != null)
            {
                material.SetTexture("_PackedSpecular", packedSpecTex);
            }
            if (normalTex != null)
            {
                material.SetTexture("_BumpMap", normalTex);
            }

            return material;
        }

        private Texture2D CreateTexture(string textureName)
        {
            if (string.IsNullOrEmpty(textureName))
            {
                return null;
            }

            DdsTexture textureData = assetManager.LoadPackAsset<DdsTexture>(textureName);

            if (textureData == null)
            {
                Debug.LogErrorFormat("Could not load texture \"{0}\"", textureName);
                return null;
            }

            Assert.IsNotNull(textureData);

            Texture2D texture = new Texture2D(textureData.Width, textureData.Height, textureData.TextureFormat, true);
            texture.LoadRawTextureData(textureData.TextureData);
            texture.Apply();

            return texture;
        }
    }
}