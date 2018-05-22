namespace WorldBuilder
{
    using System;
    using System.IO;
    using Formats;
    using Formats.Pack;

    public class AssetManager
    {
        private Pack[] assetPacks;

        public void LoadPacks()
        {

        }

        public MemoryStream CreateAssetMemoryStreamByName(string assetName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads a Pack-stored asset from the current active forgelight game.
        /// </summary>
        /// <typeparam name="T">The type of asset, and deserializer to use.</typeparam>
        /// <param name="assetName">The asset's name (with extension)</param>
        /// <returns>The deserialized asset instance.</returns>
        public T LoadPackAsset<T>(string assetName) where T : IReadableAsset, new()
        {
            // We traverse backwards as we want later packs to override previous ones.
            AssetRef asset;
            for (int i = assetPacks.Length - 1; i >= 0; i--)
            {
                if (!assetPacks[i].Assets.TryGetValue(assetName, out asset))
                {
                    continue;
                }

                // We found a matching asset. Attempt to deserialize it using the provided type.
                T retVal = new T();
                retVal.Name = assetName;
                retVal.DisplayName = Path.GetFileNameWithoutExtension(assetName) + " (" + assetPacks[i].Name + ")";

                bool result;
                using (MemoryStream assetStream = assetPacks[i].CreateAssetStream(asset))
                {
                    result = retVal.Deserialize(assetStream);
                }

                // If we succeeded, we return the result, otherwise the default value (usually null).
                return result ? retVal : default(T);
            }

            // No asset was found matching the specified name.
            return default(T);
        }

        /// <summary>
        /// Attempts to load an asset external from the current forgelight game
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadExternalAsset<T>(string path) where T : IReadableAsset, new()
        {
            throw new NotImplementedException();
        }
    }
}