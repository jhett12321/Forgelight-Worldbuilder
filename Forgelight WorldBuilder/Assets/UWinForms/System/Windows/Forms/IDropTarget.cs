namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public interface IDropTarget
    {
        void OnDragEnter(DragEventArgs e);
        void OnDragLeave(EventArgs e);
        void OnDragDrop(DragEventArgs e);
        void OnDragOver(DragEventArgs e);
    }
}
