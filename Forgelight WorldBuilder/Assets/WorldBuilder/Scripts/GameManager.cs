namespace WorldBuilder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using SFB;
    using UnityEngine;
    using WorldEditor;
    using Zenject;

    /// <summary>
    /// Handles Loading of Forgelight Games.
    /// </summary>
    public class GameManager
    {
        private const string LAST_DIR_PREF_KEY = "last_forgelight_directory";
        private static readonly string PACK_RELATIVE_DIR = "Resources" + Path.DirectorySeparatorChar + "Assets";
        public ForgelightGame ActiveGame { get; private set; }

        [Inject] private List<IEditorLoadable> loadables;
        [Inject] private StatusReporter statusReporter;
        public event Action OnGameLoaded;

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

                    await ActiveGame.LoadPacks(statusReporter);

                    Task[] loadTasks = new Task[loadables.Count];
                    for (int i = 0; i < loadables.Count; i++)
                    {
                        IEditorLoadable editorLoadable = loadables[i];
                        loadTasks[i] = editorLoadable.LoadSystem(statusReporter);
                    }

                    foreach (Task task in loadTasks)
                    {
                        await task;
                    }

                    OnGameLoaded?.Invoke();
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