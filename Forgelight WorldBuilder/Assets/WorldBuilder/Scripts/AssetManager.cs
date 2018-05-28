namespace WorldBuilder
{
    using System;
    using System.IO;
    using Formats;
    using Formats.Pack;
    using Syroot.BinaryData;
    using UnityEngine.Assertions;
    using Zenject;

    public class AssetManager
    {
        [Inject] private ForgelightGame forgelightGame;

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
            Pack[] packs = forgelightGame.Packs;

            Assert.IsNotNull(packs);
            Assert.IsFalse(packs.Length == 0);

            // We traverse backwards as we want later packs to override previous ones.
            AssetRef asset;
            for (int i = packs.Length - 1; i >= 0; i--)
            {
                Pack pack = packs[i];

                if (!pack.Assets.TryGetValue(assetName, out asset))
                {
                    continue;
                }

                // We found a matching asset. Attempt to deserialize it using the provided type.
                T retVal = new T();
                retVal.Name = asset.Name;
                retVal.DisplayName = asset.DisplayName;

                bool result;
                MemoryStream memoryStream = pack.CreateAssetStream(asset); // Disposed by BinaryStream.
                using (BinaryStream stream = new BinaryStream(memoryStream, retVal.ByteConverter))
                {
                    result = retVal.Deserialize(stream);
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