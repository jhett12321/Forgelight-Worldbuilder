namespace UWinForms.System.Windows.Forms
{
    using global::System;
    using global::System.ComponentModel;

    public class LayoutEventArgs : EventArgs
    {
        private readonly IComponent affectedComponent;
        private readonly string     affectedProperty;

        public LayoutEventArgs(IComponent affectedComponent, string affectedProperty)
        {
            this.affectedComponent = affectedComponent;
            this.affectedProperty = affectedProperty;
        }
        public LayoutEventArgs(Control affectedControl, string affectedProperty) : this((IComponent)affectedControl, affectedProperty)
        {
        }

        public IComponent AffectedComponent
        {
            get
            {
                return this.affectedComponent;
            }
        }
        public Control AffectedControl
        {
            get
            {
                return this.affectedComponent as Control;
            }
        }
        public string AffectedProperty
        {
            get
            {
                return this.affectedProperty;
            }
        }
    }
}
