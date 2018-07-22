namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public class TreeViewEventArgs : EventArgs
    {
        public TreeViewEventArgs(TreeNode node)
        {
            Action = TreeViewAction.Unknown;
            Node = node;
        }
        public TreeViewEventArgs(TreeNode node, TreeViewAction action)
        {
            Action = action;
            Node = node;
        }

        public TreeViewAction Action { get; private set; }
        public TreeNode Node { get; private set; }
    }
}
