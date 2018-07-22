namespace UWinForms.Unity.API
{
    using System.Drawing;
    using Core.API;

    public class UnityGdiSprite : ITexture
    {
        internal readonly UnityEngine.Sprite sprite;

        public UnityGdiSprite(UnityEngine.Sprite spr)
        {
            sprite = spr;
        }

        public int Height
        {
            get { return (int)sprite.rect.height; }
        }
        public int Width
        {
            get { return (int)sprite.rect.width; }
        }

        public void Apply()
        {
        }
        public void Clear(Color color)
        {
        }
        public Color GetPixel(int x, int y)
        {
            throw new global::System.NotImplementedException();
        }
        public Color[] GetPixels()
        {
            throw new global::System.NotImplementedException();
        }
        public Color[] GetPixels(int x, int y, int width, int height)
        {
            throw new global::System.NotImplementedException();
        }
        public void SetPixel(int x, int y, Color color)
        {
            throw new global::System.NotImplementedException();
        }
        public void SetPixels(Color[] colors)
        {
            throw new global::System.NotImplementedException();
        }
        public void SetPixels(int x, int y, int width, int height, Color[] colors)
        {
            throw new global::System.NotImplementedException();
        }
    }
}
