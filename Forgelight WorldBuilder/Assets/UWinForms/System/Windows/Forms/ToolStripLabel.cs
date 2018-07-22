namespace UWinForms.System.Windows.Forms
{
    using Drawing;

    public class ToolStripLabel : ToolStripItem
    {
        public ToolStripLabel()
        {
        }
        public ToolStripLabel(string text) : base(text, null, null)
        {
        }
        public ToolStripLabel(Image image) : base(null, image, null)
        {
        }
        public ToolStripLabel(string text, Image image) : base(text, image, null)
        {
        }
    }
}