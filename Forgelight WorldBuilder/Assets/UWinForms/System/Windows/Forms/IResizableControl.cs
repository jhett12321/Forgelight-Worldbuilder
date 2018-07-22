namespace UWinForms.System.Windows.Forms
{
    using Drawing;

    public enum ControlResizeTypes
    {
        None,

        Right,
        Down,
        Left,
        Up,

        RightDown,
        LeftDown,
        LeftUp,
        RightUp
    }

    public interface IResizableControl
    {
        ControlResizeTypes GetResizeAt(Point mclient);
    }
}
