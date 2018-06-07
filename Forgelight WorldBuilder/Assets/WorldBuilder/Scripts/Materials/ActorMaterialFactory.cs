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
        [Inject] private TextureManager textureManager;

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
            Texture2D diffuseTex = textureManager.GetTexture(mesh.BaseDiffuse);
            Texture2D packedSpecTex = textureManager.GetTexture(mesh.SpecMap);
            Texture2D normalTex = textureManager.GetTexture(mesh.BumpMap);

            Material material = new Material(source);

            if (diffuseTex != null)
            {
                material.SetTexture("_MainTex", diffuseTex);
                material.SetTextureScale("_MainTex", new Vector2(1, -1));
            }
            if (packedSpecTex != null)
            {
                material.SetTexture("_PackedSpecular", packedSpecTex);
                material.SetTextureScale("_PackedSpecular", new Vector2(1, -1));
            }
            if (normalTex != null)
            {
                material.SetTexture("_BumpMap", normalTex);
                material.SetTextureScale("_BumpMap", new Vector2(1, -1));
            }

            return material;
        }
    }
}