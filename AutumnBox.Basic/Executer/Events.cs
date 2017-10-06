/* =============================================================================*\
*
* Filename: Events.cs
* Description: 
*
* Version: 1.0
* Created: 9/16/2017 04:47:06(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
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
