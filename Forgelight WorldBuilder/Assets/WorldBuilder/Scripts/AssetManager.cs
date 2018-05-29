namespace WorldBuilder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Formats;
    using Formats.Pack;
    using Syroot.BinaryData;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Zenject;

    public class AssetManager
    {
        [Inject] private GameManager gameManager;

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
            Pack[] packs = gameManager.ActiveGame.Packs;

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
                return CreateAsset<T>(asset);
            }

            // No asset was found matching the specified name.
            return default(T);
        }

        public AssetRef[] GetAssetsByType(AssetType assetType, string filter = null)
        {
            Pack[] packs = gameManager.ActiveGame.Packs;

            Assert.IsNotNull(packs);
            Assert.IsFalse(packs.Length == 0);

            List<AssetRef> assets = new List<AssetRef>();
            foreach (Pack pack in packs)
            {
                foreach (AssetRef asset in pack.Assets.Values)
                {
                    if (asset.AssetType == assetType && (string.IsNullOrEmpty(filter) || asset.Name.ToLower().Contains(filter.ToLower())))
                    {
                        assets.Add(asset);
                    }
                }
            }

            return assets.ToArray();
        }

        public T CreateAsset<T>(ICollection<byte> data, string name = null, string displayName = null) where T : IReadableAsset, new()
        {
            MemoryStream stream = new MemoryStream(data.ToArray());
            return CreateAsset<T>(stream, name, displayName);
        }

        /// <summary>
        /// Creates an asset from the supplied asset reference.
        /// </summary>
        /// <typeparam name="T">The asset's serializer to use for deserialization.</typeparam>
        /// <param name="asset">The asset's reference</param>
        /// <returns>The deserialized asset, or null if deserialization failed.</returns>
        public T CreateAsset<T>(AssetRef asset) where T : IReadableAsset, new()
        {
            MemoryStream stream = asset.Pack.CreateAssetStream(asset); // Disposed by BinaryStream.
            return CreateAsset<T>(stream, asset.Name, asset.DisplayName);
        }

        public T CreateAsset<T>(Stream stream, string name = null, string displayName = null) where T : IReadableAsset, new()
        {
            bool result = false;
            T retVal = new T();

            try
            {
                retVal.Name = name;
                retVal.DisplayName = displayName;

                using (BinaryStream binaryStream = new BinaryStream(stream, retVal.ByteConverter))
                {
                    result = retVal.Deserialize(binaryStream);
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("An error occurred while creating asset \"{0}\" (See below for details)", name);
                Debug.LogException(e);
            }

            // If we succeeded, we return the result, otherwise the default value (usually null).
            return result ? retVal : default(T);
        }

        /// <summary>
        /// Attempts to load an external, non-pack asset of the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadExternalAsset<T>(string path) where T : IReadableAsset, new()
        {
            FileStream fStream = File.OpenRead(path);
            return CreateAsset<T>(fStream, Path.GetFileName(path), Path.GetFileNameWithoutExtension(path));
        }
    }
}