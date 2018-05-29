namespace WorldBuilder
{
    using System;
    using System.IO;
    using SFB;
    using UnityEngine;

    /// <summary>
    /// Handles Loading of Forgelight Games.
    /// </summary>
    public class GameManager
    {
        private const string LAST_DIR_PREF_KEY = "last_forgelight_directory";
        private static readonly string PACK_RELATIVE_DIR = "Resources" + Path.DirectorySeparatorChar + "Assets";
        public ForgelightGame ActiveGame { get; private set; }

        public event GameLoadedEvent OnGameLoaded;
        public delegate void GameLoadedEvent(ForgelightGame game);

        public void SwitchGame()
        {
            string startPath = PlayerPrefs.GetString(LAST_DIR_PREF_KEY, "");

            StandaloneFileBrowser.OpenFolderPanelAsync("Select Forgelight Directory", startPath, false, async paths =>
            {
                if (paths == null || paths.Length == 0)
                {
                    return;
                }

                string path = paths[0];
                string packPath = Path.Combine(path, PACK_RELATIVE_DIR);

                if (!Directory.Exists(packPath))
                {
                    Debug.LogWarningFormat("Path \"{0}\" is not a valid Forgelight Game directory.", path);
                    return;
                }

                try
                {
                    ActiveGame = new ForgelightGame(Path.GetDirectoryName(path), packPath);
                    await ActiveGame.LoadPacks(new Progress<int>(progress =>
                    {
                        Debug.LogFormat("Pack Load Progress: {0}%", progress);
                    }));

                    OnGameLoaded?.Invoke(ActiveGame);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("An error occurred while loading Forgelight Game \"{0}\". {1}", path, e);
                    throw;
                }
            });
        }
    }
}