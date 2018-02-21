/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 2:33:11
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public delegate void ProcessStartedEventHandler(object sender, ProcessStartedEventArgs e);
    public class ProcessStartedEventArgs : EventArgs
    {
        public int Pid { get; set; }
        public ProcessStartedEventArgs() { }
        public ProcessStartedEventArgs(int pid)
        {
            this.Pid = pid;
        }
    }
}
