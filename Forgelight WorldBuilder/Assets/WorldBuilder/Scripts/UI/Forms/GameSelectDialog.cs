namespace WorldBuilder.UI
{
    using System.Windows.Forms;
    using FileDialog = UWinForms.System.Windows.Forms.FileDialog;

    public class GameSelectDialog : FileDialog
    {
        public GameSelectDialog()
        {
            Filter = "Forgelight Games (*.exe)|*.exe";
            Name = "Select Forgelight Game";

            Text = "Locate Forgelight Game";
            buttonOk.Text = "Select";
        }
    }
}