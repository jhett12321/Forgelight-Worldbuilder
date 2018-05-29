namespace WorldBuilder.Materials
{
    using Formats.Cnk;
    using Formats.Dma;
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

        public Material GetMaterial(CnkLOD chunkData)
        {
            Material material = new Material(source);
            Texture2D[] diffuseTextures = new Texture2D[chunkData.Textures.Count];
            Texture2D[] specTextures = new Texture2D[chunkData.Textures.Count];

            for (int i = 0; i < chunkData.Textures.Count; i++)
            {
                CnkLOD.Texture texture = chunkData.Textures[i];

                DdsTexture diffuse = assetManager.CreateAsset<DdsTexture>(texture.ColorNXMap);
                DdsTexture spec = assetManager.CreateAsset<DdsTexture>(texture.SpecNyMap);

                Texture2D diffuseTex = new Texture2D(diffuse.Width, diffuse.Height, diffuse.TextureFormat, false);
                Texture2D specTex = new Texture2D(spec.Width, spec.Height, spec.TextureFormat, false);

                diffuseTex.LoadRawTextureData(diffuse.TextureData);
                specTex.LoadRawTextureData(spec.TextureData);
                diffuseTextures[i] = diffuseTex;
                specTextures[i] = specTex;
            }

            Texture2D materialDiffuse = new Texture2D(1024, 1024);
            materialDiffuse.PackTextures(diffuseTextures, 0);

            Texture2D materialSpec = new Texture2D(1024, 1024);
            materialSpec.PackTextures(specTextures, 0);

            material.SetTexture("_MainTex", materialDiffuse);
            material.SetTexture("_PackedSpecular", materialSpec);

            return material;
        }

        //private Material CreateMaterial(CnkLOD.Texture texture)
        //{
        //    Material material = new Material(source);

        //    DdsTexture diffuse = assetManager.CreateAsset<DdsTexture>(texture.ColorNXMap);
        //    DdsTexture spec = assetManager.CreateAsset<DdsTexture>(texture.SpecNyMap);

        //    Texture2D diffuseTex = new Texture2D(512, 512, diffuse.TextureFormat, false);
        //    Texture2D specTex = new Texture2D(512, 512, spec.TextureFormat, false);

        //    diffuseTex.LoadRawTextureData(diffuse.TextureData);
        //    specTex.LoadRawTextureData(spec.TextureData);

        //    material.SetTexture("_MainTex", diffuseTex);
        //    material.SetTexture("_PackedSpecular", specTex);

        //    return material;
        //}
    }
}