namespace WorldBuilder
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Formats.Pack;
    using Syroot.BinaryData;
    using UnityEngine;

    /// <summary>
    /// Represents a loaded Forgelight Game.
    /// </summary>
    public class ForgelightGame
    {
        private const float MAX_FRAME_TIME = 0.04f;

        public readonly string Name;
        public Pack[] Packs { get; private set; }

        private readonly string packDir;

        public ForgelightGame(string name, string packDir)
        {
            Name = name;
            this.packDir = packDir;
        }

        public async Task LoadPacks(IProgress<int> progress)
        {
            string[] packPaths = Directory.GetFiles(packDir, "*.pack", SearchOption.TopDirectoryOnly);
            Packs = new Pack[packPaths.Length];

            float frameStart = Time.realtimeSinceStartup;
            for (int i = 0; i < packPaths.Length; i++)
            {
                string path = packPaths[i];
                FileStream fStream = File.OpenRead(path); // Disposed by BinaryStream.
                using (BinaryStream binaryStream = new BinaryStream(fStream))
                {
                    Pack pack = new Pack(path);
                    binaryStream.ByteConverter = pack.ByteConverter;

                    pack.Deserialize(binaryStream);

                    Packs[i] = pack;
                }

                progress.Report(i * 100 / packPaths.Length);

                if (Time.realtimeSinceStartup - frameStart > MAX_FRAME_TIME)
                {
                    frameStart = Time.realtimeSinceStartup;
                    await new WaitForEndOfFrame();
                }
            }
        }
    }
}