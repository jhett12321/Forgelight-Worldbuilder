namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public class FormClosedEventArgs : EventArgs
    {
        public FormClosedEventArgs(CloseReason closeReason)
        {
            CloseReason = closeReason;
        }

        public CloseReason CloseReason { get; private set; }
    }
}
