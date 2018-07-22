namespace UWinForms.System.Windows.Forms
{
    using global::System;
    using global::System.ComponentModel;

    public class ContextMenuStrip : ToolStripDropDownMenu
    {
        public ContextMenuStrip()
        {
        }
        public ContextMenuStrip(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            container.Add(this);
        }
    }
}
