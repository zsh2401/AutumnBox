using System;

namespace AutumnBox.GUI.Services
{
    public sealed class SubWindowFinishedEventArgs : EventArgs
    {
        public SubWindowFinishedEventArgs(object value = null)
        {
            Value = value;
        }

        public object Value { get; }
    }
    interface ISubWindowDialog
    {
        event EventHandler<SubWindowFinishedEventArgs> Finished;
        object View { get; }
    }
}
