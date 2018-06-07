namespace WorldBuilder
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Formats.Pack;
    using Syroot.BinaryData;
    using WorldEditor;

    /// <summary>
    /// Represents a loaded Forgelight Game.
    /// </summary>
    public class ForgelightGame
    {
        public readonly string Name;
        public Pack[] Packs { get; private set; }

        private readonly string packDir;

        public ForgelightGame(string name, string packDir)
        {
            Name = name;
            this.packDir = packDir;
        }

        public async Task LoadPacks(StatusReporter progress)
        {
            string[] packPaths = Directory.GetFiles(packDir, "*.pack", SearchOption.TopDirectoryOnly);
            Packs = new Pack[packPaths.Length];
            Task<Pack>[] packTasks = new Task<Pack>[packPaths.Length];

            for (int i = 0; i < packPaths.Length; i++)
            {
                string path = packPaths[i];
                packTasks[i] = LoadPack(path);
            }

            for (int i = 0; i < packTasks.Length; i++)
            {
                Packs[i] = await packTasks[i];
                progress.ReportProgress("Loading Packs", i, packPaths.Length);
            }
        }

        public async Task<Pack> LoadPack(string path)
        {
            await new WaitForBackgroundThread();

            FileStream fStream = File.OpenRead(path); // Disposed by BinaryStream.
            using (BinaryStream binaryStream = new BinaryStream(fStream))
            {
                Pack pack = new Pack(path);
                binaryStream.ByteConverter = pack.ByteConverter;

                pack.Deserialize(binaryStream);

                return pack;
            }
        }
    }
}