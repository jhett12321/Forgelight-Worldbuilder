namespace WorldBuilder.Materials
{
    using Formats.Cnk;
    using Formats.Dma;
    using Formats.Textures;
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
            Texture2D[] diffuseTextures = new Texture2D[chunkData.Textures.Count];
            Texture2D[] specTextures = new Texture2D[chunkData.Textures.Count];

            for (int i = 0; i < chunkData.Textures.Count; i++)
            {
                CnkLOD.Texture texture = chunkData.Textures[i];

                Dds diffuse = texture.ColorNXMap;
                Dds spec = texture.SpecNyMap;

                Texture2D diffuseTex = new Texture2D(diffuse.Width, diffuse.Height, diffuse.TextureFormat, false);
                Texture2D specTex = new Texture2D(spec.Width, spec.Height, spec.TextureFormat, false);

                diffuseTex.LoadRawTextureData(diffuse.TextureData.Data);
                specTex.LoadRawTextureData(spec.TextureData.Data);

                diffuseTextures[i] = diffuseTex;
                specTextures[i] = specTex;
            }

            Texture2D materialDiffuse = new Texture2D(1024, 1024);
            materialDiffuse.PackTextures(diffuseTextures, 0, 2048, true);
            materialDiffuse.name = chunkData.Name + " Diffuse";

            Texture2D materialSpec = new Texture2D(1024, 1024);
            materialSpec.PackTextures(specTextures, 0, 2048, true);
            materialSpec.name = chunkData.Name + " Specular";

            Material material = new Material(source);
            material.SetTexture("_MainTex", materialDiffuse);
            material.SetTextureScale("_MainTex", new Vector2(1, -1));
            material.SetTexture("_PackedSpecular", materialSpec);
            material.SetTextureScale("_PackedSpecular", new Vector2(1, -1));

            return material;
        }
    }
}