namespace UWinForms.System.Windows.Forms
{
    using global::System;

    [Flags]
    public enum LinkState
    {
        Normal = 0x00,
        Hover = 0x01,
        Active = 0x02,
        Visited = 0x04
    }
}
