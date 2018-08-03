namespace WorldBuilder.Sky
{
    using Formats.Textures;
    using Formats.Xml.Sky;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Attempts to find a skybox for the current zone.
    /// </summary>
    public class SkyFactory
    {
        [Inject] private AssetManager assetManager;
        // TODO Consider Time-of-day options.
        // TODO Process Sky.xml

        private Material skyboxInstance;

        public SkyFactory()
        {
            skyboxInstance = Object.Instantiate(RenderSettings.skybox);
            RenderSettings.skybox = skyboxInstance;
        }

        public void UpdateSky(string zoneName)
        {
            Tga tex = assetManager.LoadPackAsset<Tga>($"sky_{zoneName.ToLower()}_dome_1200.tga");

            if (tex == null)
            {
                Debug.LogWarning($"Could not find Sky for Zone {zoneName}. Using Default.");
                return;
            }

            Texture2D skybox = new Texture2D(tex.Width, tex.Height, TextureFormat.ARGB32, false);
            skybox.SetPixels32(tex.Data);
            skybox.Apply(false, true);

            skyboxInstance.SetTexture("_Tex", skybox);
            DynamicGI.UpdateEnvironment();
        }
    }
}