namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public class KeyPressEventArgs : EventArgs
    {
        public char KeyChar { get; set; }
        public bool Handled { get; set; }

        public KeyEventArgs uwfKeyArgs;

        public KeyPressEventArgs(char keyChar)
        {
            KeyChar = keyChar;
        }
    }
}
