namespace AutumnBox.Basic.Executer
{
    using System;
    public class ProcessStartedEventArgs : EventArgs {
        public ProcessStartedEventArgs(int pid) {
            PID = pid;
        }
        public ProcessStartedEventArgs() { }
        public int PID { get; internal set; }
    }
    public delegate void ProcessStartedEventHandler(object sender, ProcessStartedEventArgs e);
}
