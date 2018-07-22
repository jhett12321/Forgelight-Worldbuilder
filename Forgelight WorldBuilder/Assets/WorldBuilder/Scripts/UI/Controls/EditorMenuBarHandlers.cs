namespace WorldBuilder.UI.Controls
{
    using System;
    using Formats.Pack;
    using SFB;
    using UnityEngine;
    using Zenject;
    using Zone;
    using DialogResult = UWinForms.System.Windows.Forms.DialogResult;
    using OpenFileDialog = UWinForms.System.Windows.Forms.OpenFileDialog;

    public partial class EditorMenuBar
    {
        private const string LAST_DIR_PREF_KEY = "last_forgelight_directory";

        [Inject] private GameManager gameManager;
        [Inject] private ZoneFactory zoneFactory;
        [Inject] private DiContainer container;

        [Inject]
        private void RegisterEventListeners()
        {
            UpdateEditorState(gameManager.HasLoadedGame, zoneFactory.ZoneLoaded);
            gameManager.OnGameLoaded += () => UpdateEditorState(true, zoneFactory.ZoneLoaded);
            FileMenuClickHandlers();
        }

        private void FileMenuClickHandlers()
        {
            MenuFile.LoadGame.Click += LoadGameOnClick;
            MenuFile.Open.Click += OpenOnClick;
            MenuFile.OpenFromPack.Click += OpenFromPackOnClick;
            MenuFile.Exit.Click += ExitOnClick;
        }

        /// <summary>
        /// Opens an externally saved zone file.
        /// </summary>
        private async void OpenOnClick(object sender, EventArgs e)
        {
            string startPath = PlayerPrefs.GetString(LAST_DIR_PREF_KEY, "");
            StandaloneFileBrowser.OpenFilePanelAsync("Open Zone File", startPath, "zone", false, paths =>
            {
                if (paths == null || paths.Length == 0)
                {
                    return;
                }

                string path = paths[0];
                zoneFactory.LoadZoneFromFile(path);
            });

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Forgelight Zone (*.zone)|*.zone";
            Name = "Open Zone File";

            dialog.ShowDialog((form, result) =>
            {
                if (result == DialogResult.OK)
                {
                    zoneFactory.LoadZoneFromFile(dialog.FileName);
                }
            });
        }

        /// <summary>
        /// Opens a zone file stored in pack assets.
        /// </summary>
        private async void OpenFromPackOnClick(object sender, EventArgs e)
        {
            await new WaitForUpdate();
            AssetPicker picker = new AssetPicker(AssetType.ZONE, "Load Zone");
            container.Inject(picker);
            picker.Show();
            picker.Focus();

            picker.OnAssetPicked += asset => zoneFactory.LoadZoneFromPacks(asset);
        }

        private void ExitOnClick(object sender, EventArgs e)
        {
            #if UNITY_EDITOR
            UnityEngine.Application.Quit();
            #else
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        /// <summary>
        /// Opens a File Dialog to locate a forgelight game.
        /// The result is passed to the game manager for loading the game.
        /// </summary>
        private void LoadGameOnClick(object sender, EventArgs e)
        {
            string startPath = PlayerPrefs.GetString(LAST_DIR_PREF_KEY, "");
            StandaloneFileBrowser.OpenFolderPanelAsync("Select Forgelight Directory", startPath, false, paths =>
            {
                if (paths == null || paths.Length == 0)
                {
                    return;
                }

                string path = paths[0];

                PlayerPrefs.SetString(LAST_DIR_PREF_KEY, path);
                gameManager.SwitchGame(path);
            });
        }
    }
}