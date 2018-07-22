namespace UWinForms.System.Windows.Forms
{
    using Drawing;

    public class PaintEventArgs
    {
        public Rectangle ClipRectangle { get; set; }
        public Graphics Graphics { get; set;  }
    }
}
