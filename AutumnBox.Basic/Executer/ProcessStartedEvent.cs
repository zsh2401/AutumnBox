/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 2:33:11
** desc： ...
*************************************************/
using System;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 当进程开始时的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ProcessStartedEventHandler(object sender, ProcessStartedEventArgs e);
    /// <summary>
    /// 当进程开始时的事件处理器参数
    /// </summary>
    public class ProcessStartedEventArgs : EventArgs
    {
        /// <summary>
        /// 进程ID
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="pid"></param>
        public ProcessStartedEventArgs(int pid)
        {
            this.Pid = pid;
        }
    }
}
