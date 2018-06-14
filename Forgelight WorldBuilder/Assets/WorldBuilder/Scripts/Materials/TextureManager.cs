namespace WorldBuilder.Materials
{
    using System.Collections.Generic;
    using Formats.Dma;
    using ModestTree;
    using UnityEngine;
    using Zenject;

    public class TextureManager
    {
        [Inject] private AssetManager assetManager;

        private Dictionary<string, Texture2D> Textures { get; } = new Dictionary<string, Texture2D>();

        public Texture2D GetTexture(string textureName)
        {
            if (string.IsNullOrEmpty(textureName))
            {
                return null;
            }

            Texture2D texture;
            if (!Textures.TryGetValue(textureName, out texture))
            {
                DdsTexture textureData = assetManager.LoadPackAsset<DdsTexture>(textureName);

                if (textureData == null)
                {
                    Debug.LogErrorFormat("Could not load texture \"{0}\"", textureName);
                    Textures.Add(textureName, null);
                    return null;
                }

                texture = new Texture2D(textureData.Width, textureData.Height, textureData.TextureFormat, true);
                texture.name = textureName;
                texture.LoadRawTextureData(textureData.TextureData.Data);
                texture.Apply(true, true);

                assetManager.Dispose(textureData);

                Textures.Add(textureName, texture);
            }

            return texture;
        }

    }
}