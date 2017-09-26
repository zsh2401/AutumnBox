using System;

namespace AutumnBox.Basic.Executer
{
    public class ProcessStartedEventArgs : EventArgs {
        public int PID { get; internal set; }
    }
    public delegate void ProcessStartedEventHandler(object sender, ProcessStartedEventArgs e);
}
