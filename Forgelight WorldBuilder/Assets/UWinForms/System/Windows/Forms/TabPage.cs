namespace UWinForms.System.Windows.Forms
{
    using Drawing;

    public class TabPage : Panel
    {
        public TabPage()
        {
            BackColor = Color.White;
        }
        public TabPage(string text) : this()
        {
            Text = text;
        }

        public int ImageIndex { get; set; }
        public string ImageKey { get; set; }
    }
}
