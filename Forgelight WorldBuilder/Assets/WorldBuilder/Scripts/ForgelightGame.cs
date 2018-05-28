namespace WorldBuilder
{
    using System.IO;
    using Formats.Pack;
    using Syroot.BinaryData;

    public class ForgelightGame
    {
        private static readonly string PACK_RELATIVE_DIR = "Resources" + Path.DirectorySeparatorChar + "Assets";

        public string Name;
        public Pack[] Packs;

        public void SwitchActiveGame(string newGamePath)
        {
            LoadPackFiles(Path.Combine(newGamePath, PACK_RELATIVE_DIR));
        }

        private void LoadPackFiles(string packPath)
        {
            string[] packPaths = Directory.GetFiles(packPath, "*.pack", SearchOption.TopDirectoryOnly);
            Packs = new Pack[packPaths.Length];

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
            }
        }
    }
}