namespace UWinForms.System.Windows.Forms
{
    using global::System;

    [Flags]
    public enum BoundsSpecified
    {
        X = 1,
        Y = 2,
        Width = 4,
        Height = 8,
        Location = Y | X,
        Size = Height | Width,
        All = Size | Location,
        None = 0,
    }
}
