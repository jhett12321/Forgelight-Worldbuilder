namespace WorldBuilder.UI
{
    using Controls;
    using UnityEngine;
    using Zenject;
    using Color = UWinForms.System.Drawing.Color;
    using Form = UWinForms.System.Windows.Forms.Form;
    using FormBorderStyle = UWinForms.System.Windows.Forms.FormBorderStyle;
    using Screen = UWinForms.System.Windows.Forms.Screen;

    /// <summary>
    /// Renders the main editor window controls.
    /// </summary>
    public class MainEditor : Form, IInitializable
    {
        [Inject] private DiContainer container;
        [Inject] private ResolutionObserver resObs;

        public EditorMenuBar MenuBar { get; set; }

        public MainEditor()
        {
            // Components
            MenuBar = new EditorMenuBar();
            MenuBar.Width = Width;

            Controls.Add(MenuBar);
            MainMenuStrip = MenuBar;

            // Form Settings
            BackColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ControlBox = false;
            Bounds = Screen.PrimaryScreen.WorkingArea;
            uwfHeaderHeight = 0;
        }

        public void Initialize()
        {
            container.Inject(MenuBar);

            Show();

            resObs.OnResolutionChanged += OnResolutionChanged;
        }

        private void OnResolutionChanged(Resolution lastRes, Resolution newRes)
        {
            Width = newRes.width;
            Height = newRes.height;

            MenuBar.Width = newRes.width;
        }
    }
}