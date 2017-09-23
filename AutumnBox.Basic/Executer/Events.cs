using System;

namespace AutumnBox.Basic.Executer
{
    public class ProcessStartEventArgs : EventArgs {
        public int PID { get; internal set; }
    }
    public delegate void ProcessStartEventHandler(object sender, ProcessStartEventArgs e);
}
