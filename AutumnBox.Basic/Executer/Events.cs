using System;

namespace AutumnBox.Basic.Executer
{
    public class ExecuteStartEventArgs : EventArgs
    {
        public int PID { get; internal set; }
        public string Command { get; internal set; }
    }
    public class ProcessStartEventArgs : EventArgs {
        public int PID { get; internal set; }
    }
    public delegate void ProcessStartEventHandler(object sender, ProcessStartEventArgs e);
    public delegate void ExecuteStartHandler(object sender, ExecuteStartEventArgs e);
}
