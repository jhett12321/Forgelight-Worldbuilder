namespace WorldBuilder.Formats.Pack
{
    using System;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine;

    public class AssetRef
    {
        public Pack Pack { get; private set; }

        /// <summary>
        /// The name (+extension) of this asset.
        /// </summary>
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public uint Size { get; private set; }
        public uint AbsoluteOffset { get; private set; }
        public uint Crc32 { get; private set; }

        public AssetType AssetType { get; private set; }

        private AssetRef(Pack pack)
        {
            Pack = pack;
            Name = string.Empty;
            Size = 0;
            AbsoluteOffset = 0;
            AssetType = AssetType.Unknown;
        }

        public static AssetRef LoadBinary(Pack pack, BinaryStream stream)
        {
            AssetRef assetRef;

            assetRef = new AssetRef(pack);

            uint count = stream.ReadUInt32();
            assetRef.Name = stream.ReadString((int)count);
            assetRef.DisplayName = assetRef.Name + " (" + pack.DisplayName + ')';
            assetRef.AbsoluteOffset = stream.ReadUInt32();
            assetRef.Size = stream.ReadUInt32();
            assetRef.Crc32 = stream.ReadUInt32();

            // Set the type of the asset based on the extension
            // First get the extension without the leading '.'
            string extension = Path.GetExtension(assetRef.Name).Substring(1);

            try
            {
                assetRef.AssetType = (AssetType) Enum.Parse(typeof (AssetType), extension, true);
            }
            catch (ArgumentException)
            {
                // This extension isn't mapped in the enum
                Debug.LogWarning("Unknown Forgelight File Type: " + extension);
                assetRef.AssetType = AssetType.Unknown;
            }

            return assetRef;
        }

        public override string ToString() => Name;
    }
}