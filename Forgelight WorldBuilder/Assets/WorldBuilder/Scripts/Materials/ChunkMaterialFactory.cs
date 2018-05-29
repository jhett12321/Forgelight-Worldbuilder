namespace WorldBuilder.Materials
{
    using System.IO;
    using Formats.Cnk;
    using Formats.Dma;
    using Syroot.BinaryData;
    using UnityEngine;
    using Zenject;
    using Material = UnityEngine.Material;

    public class ChunkMaterialFactory
    {
        private Material source;
        [Inject] private AssetManager assetManager;

        public ChunkMaterialFactory(Material source)
        {
            this.source = source;
        }

        public Material[] GetMaterials(CnkLOD chunkData)
        {
            Material[] materials = new Material[chunkData.Textures.Count];

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = CreateMaterial(chunkData.Textures[i]);
            }

            return materials;
        }

        private Material CreateMaterial(CnkLOD.Texture texture)
        {
            Material material = new Material(source);

            DdsTexture diffuse = assetManager.CreateAsset<DdsTexture>(texture.ColorNXMap);
            DdsTexture spec = assetManager.CreateAsset<DdsTexture>(texture.SpecNyMap);

            Texture2D diffuseTex = new Texture2D(diffuse.Width, diffuse.Height, diffuse.TextureFormat, false);
            Texture2D specTex = new Texture2D(diffuse.Width, diffuse.Height, spec.TextureFormat, false);

            diffuseTex.LoadRawTextureData(diffuse.TextureData);
            specTex.LoadRawTextureData(spec.TextureData);

            material.SetTexture("_MainTex", diffuseTex);
            material.SetTexture("_PackedSpecular", specTex);

            return material;
        }
    }
}