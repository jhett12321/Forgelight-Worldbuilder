namespace UWinForms.Examples.Panels
{
    using System.Drawing;
    using Unity.Controls;

    public class PanelColorPicker : BaseExamplePanel
    {
        public override void Initialize()
        {
            var picker = this.Create<ColorPicker>();
            picker.Color = Color.FromArgb(64, Color.LightGreen);
        }
    }
}