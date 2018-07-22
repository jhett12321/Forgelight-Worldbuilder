namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public class ToolStripItemClickedEventArgs : EventArgs
    {
        private ToolStripItem clickedItem;

        public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
        {
            this.clickedItem = clickedItem;
        }

        public ToolStripItem ClickedItem { get { return clickedItem; } }
    }
}
