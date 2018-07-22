namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public delegate void TreeNodeMouseHoverEventHandler(object sender, TreeNodeMouseHoverEventArgs e);

    public class TreeNodeMouseHoverEventArgs : EventArgs
    {
        public TreeNodeMouseHoverEventArgs(TreeNode node)
        {
            Node = node;
        }

        public TreeNode Node { get; private set; }
    }
}
