/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 2:32:40
** desc： ...
*************************************************/
using System;
using System.Diagnostics;

namespace AutumnBox.Basic.Data
{
    /// <summary>
    /// 接收到输出时的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void OutputReceivedEventHandler(object sender, OutputReceivedEventArgs e);
    /// <summary>
    /// 接收到输出时的事件处理的参数
    /// </summary>
    public class OutputReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 这个输出是否是标准错误
        /// </summary>
        public bool IsError { get; private set; }
        /// <summary>
        /// 具体的输出文本
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// 源事件参数
        /// </summary>
        public DataReceivedEventArgs SourceArgs { get; private set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="sourceEventArgs"></param>
        /// <param name="isError"></param>
        public OutputReceivedEventArgs(DataReceivedEventArgs sourceEventArgs, bool isError = false) {
            IsError = isError;
            SourceArgs = sourceEventArgs;
            Text = sourceEventArgs.Data;
        }
    }
    internal class CaoNa_I_Miss_You { }
}
