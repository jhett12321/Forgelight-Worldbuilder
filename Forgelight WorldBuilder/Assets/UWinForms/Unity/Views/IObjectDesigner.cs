namespace UWinForms.Unity.Views
{
    public interface IObjectDesigner
    {
        object Value { get; }

        object Draw(int width, int height);
    }
}
