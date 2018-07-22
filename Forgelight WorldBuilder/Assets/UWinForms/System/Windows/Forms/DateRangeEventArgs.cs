namespace UWinForms.System.Windows.Forms
{
    using global::System;

    public class DateRangeEventArgs : EventArgs
    {
        public DateRangeEventArgs(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime End { get; private set; }
        public DateTime Start { get; private set; }
    }
}
