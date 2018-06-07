namespace WorldBuilder.Formats.Pack
{
    using System.Collections.Generic;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine.Assertions;

    public class Pack : IReadableAsset
    {
        public string Path { get; private set; }
        public readonly Dictionary<string, AssetRef> Assets = new Dictionary<string, AssetRef>();

        public ByteConverter ByteConverter => ByteConverter.Big;

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public bool Deserialize(BinaryStream stream)
        {
            uint nextChunkAbsoluteOffset = 0;

            do
            {
                stream.Seek(nextChunkAbsoluteOffset, SeekOrigin.Begin);

                nextChunkAbsoluteOffset = stream.ReadUInt32();
                uint fileCount = stream.ReadUInt32();

                for (uint i = 0; i < fileCount; ++i)
                {
                    AssetRef file = AssetRef.LoadBinary(this, stream);
                    Assets[file.Name] = file;
                }
            } while (nextChunkAbsoluteOffset != 0);

            return true;
        }

        public Pack(string path)
        {
            Path = path;
            Name = path;
            DisplayName = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public AssetStream CreateAssetStreamByName(string name)
        {
            AssetRef assetRef;

            if (!Assets.TryGetValue(name, out assetRef))
            {
                return null;
            }

            return CreateAssetStream(assetRef);
        }

        public AssetStream CreateAssetStream(AssetRef assetRef)
        {
            Assert.IsNotNull(assetRef, "No asset reference was provided!");

            FileStream file = File.Open(assetRef.Pack.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return new AssetStream(file, assetRef);
        }

        public override string ToString() => Name;
    }
}