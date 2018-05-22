namespace WorldBuilder.Formats.Pack
{
    using System.Collections.Generic;
    using System.IO;
    using Syroot.BinaryData;
    using UnityEngine.Assertions;

    public class Pack
    {
        public string Path { get; private set; }

        public string Name => System.IO.Path.GetFileName(Path);

        public Dictionary<string, AssetRef> Assets = new Dictionary<string, AssetRef>();

        private Pack(string path)
        {
            Path = path;
        }

        public static Pack LoadBinary(string path)
        {
            Pack pack = new Pack(path);

            using (FileStream fileStream = File.OpenRead(path))
            using (BinaryStream binaryStream = new BinaryStream(fileStream, ByteConverter.Big))
            {
                uint nextChunkAbsoluteOffset = 0;

                do
                {
                    fileStream.Seek(nextChunkAbsoluteOffset, SeekOrigin.Begin);

                    nextChunkAbsoluteOffset = binaryStream.ReadUInt32();
                    uint fileCount = binaryStream.ReadUInt32();

                    for (uint i = 0; i < fileCount; ++i)
                    {
                        AssetRef file = AssetRef.LoadBinary(pack, fileStream);

                        pack.Assets[file.Name] = file;
                    }
                } while (nextChunkAbsoluteOffset != 0);
            }

            return pack;
        }

        public MemoryStream CreateAssetStreamByName(string name)
        {
            AssetRef assetRef;

            if (!Assets.TryGetValue(name, out assetRef))
            {
                return null;
            }

            return CreateAssetStream(assetRef);
        }

        public MemoryStream CreateAssetStream(AssetRef assetRef)
        {
            Assert.IsNotNull(assetRef, "No asset reference was provided!");

            FileStream file = File.Open(assetRef.Pack.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[assetRef.Size];

            file.Seek(assetRef.AbsoluteOffset, SeekOrigin.Begin);
            file.Read(buffer, 0, (int) assetRef.Size);

            MemoryStream memoryStream = new MemoryStream(buffer);

            return memoryStream;
        }

        public override string ToString() => Name;
    }
}