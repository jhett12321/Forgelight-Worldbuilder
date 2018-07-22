namespace UWinForms.Core.API
{
    using System.Drawing;
    using global::System.Globalization;

    public interface IApiSystem
    {
        CultureInfo CurrentCulture { get; }
        Point MousePosition { get; }
    }
}
