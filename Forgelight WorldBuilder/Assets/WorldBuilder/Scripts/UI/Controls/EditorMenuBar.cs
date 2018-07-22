namespace WorldBuilder.UI.Controls
{
    using System.Windows.Forms;
    using Keys = UWinForms.System.Windows.Forms.Keys;
    using MenuStrip = UWinForms.System.Windows.Forms.MenuStrip;
    using ToolStripMenuItem = UWinForms.System.Windows.Forms.ToolStripMenuItem;
    using ToolStripSeparator = UWinForms.System.Windows.Forms.ToolStripSeparator;

    /// <summary>
    /// Represents the drop-down title strip (File, Edit, etc).
    /// </summary>
    public partial class EditorMenuBar : UWinForms.System.Windows.Forms.MenuStrip
    {
        public EditorMenuBar()
        {
            MenuFile = new FileMenu();
            BuildMenu();
        }

        private void BuildMenu()
        {
            AutoSize = false;

            Items.Add(MenuFile);
            Items.Add(new ToolStripMenuItem("Edit"));
        }

        // Components
        public FileMenu MenuFile { get; }
        public class FileMenu : UWinForms.System.Windows.Forms.ToolStripMenuItem
        {
            public ToolStripMenuItem New;
            public ToolStripMenuItem Open;
            public ToolStripMenuItem OpenFromPack;
            public ToolStripMenuItem LoadGame;
            public ToolStripMenuItem Save;
            public ToolStripMenuItem SaveAs;
            public ToolStripMenuItem Exit;

            public FileMenu()
            {
                CreateComponents();
                SetLayout();
                SetShortcuts();
            }

            private void CreateComponents()
            {
                Text = "File";

                // TODO Implement
                New = new ToolStripMenuItem("New Zone (Not Implemented)");
                Open = new ToolStripMenuItem("Open Zone From File");
                OpenFromPack = new ToolStripMenuItem("Open Zone From Pack");
                LoadGame = new ToolStripMenuItem("Select Game");
                Save = new ToolStripMenuItem("Save Zone (Not Implemented)");
                SaveAs = new ToolStripMenuItem("Save Zone as... (Not Implemented)");
                Exit = new ToolStripMenuItem("Exit");
            }

            private void SetLayout()
            {
                DropDownItems.Add(New);
                DropDownItems.Add(Open);
                DropDownItems.Add(OpenFromPack);
                DropDownItems.Add(new ToolStripSeparator());
                DropDownItems.Add(Save);
                DropDownItems.Add(new ToolStripSeparator());
                DropDownItems.Add(LoadGame);
                DropDownItems.Add(new ToolStripSeparator());
                DropDownItems.Add(Exit);
            }

            private void SetShortcuts()
            {
                New.ShortcutKeys = Keys.Control | Keys.N;
                Open.ShortcutKeys = Keys.Control | Keys.O;
            }
        }

        // Public Methods
        public void UpdateEditorState(bool gameLoaded, bool zoneLoaded)
        {
            // File Menu
            MenuFile.New.Enabled = gameLoaded;
            MenuFile.Open.Enabled = gameLoaded;
            MenuFile.OpenFromPack.Enabled = gameLoaded;
            // Load Game is always enabled.
            MenuFile.Save.Enabled = zoneLoaded;
            // Exit is always enabled.
        }
    }
}