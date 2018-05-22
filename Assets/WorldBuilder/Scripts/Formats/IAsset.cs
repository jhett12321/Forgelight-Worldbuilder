namespace WorldBuilder.Formats
{
    using System.IO;

    public interface IAsset
    {
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
        bool Deserialize(Stream stream);
    }

    public interface IWritableAsset : IAsset
    {
        bool Serialize(Stream stream);
    }
}
