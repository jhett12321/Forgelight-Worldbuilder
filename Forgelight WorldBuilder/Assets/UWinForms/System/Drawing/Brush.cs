namespace UWinForms.System.Drawing
{
    using global::System;

    public abstract class Brush : ICloneable, IDisposable
    {
        public abstract object Clone();
        public void Dispose()
        {
        }
    }
}
