namespace WorldBuilder.Formats
{
    using System.IO;
    using Syroot.BinaryData;

    public interface IAsset
    {
        ByteConverter ByteConverter { get; }

        /// <summary>
        /// The base name of this asset, as referenced in pack files.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The display name of this asset. Does not include extension, and appends the asset's origin pack.
        /// </summary>
        string DisplayName { get; set; }
    }

    public interface IReadableAsset : IAsset
    {
        bool Deserialize(BinaryStream stream, AssetManager assetManager);
    }

    public interface IWritableAsset : IAsset
    {
        bool Serialize(Stream stream);
    }
}
