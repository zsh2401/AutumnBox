using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 命令执行完毕的事件处理函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CommandExecutedEventHandler(object sender, CommandExecutedEventArgs e);
    /// <summary>
    /// 命令开始执行的事件处理函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CommandExecutingEventHandler(object sender, CommandExecutingEventArgs e);
    /// <summary>
    /// 命令开始执行的事件参数
    /// </summary>
    public class CommandExecutedEventArgs : EventArgs
    {
        /// <summary>
        /// 命令执行使用的时间
        /// </summary>
        TimeSpan UsedTime { get; }
        /// <summary>
        /// 命令执行的结果
        /// </summary>
        ICommandResult Result { get; }
        /// <summary>
        /// 命令的目标文件名
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Args { get; }
        /// <summary>
        /// 构造一个参数
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="args">参数</param>
        /// <param name="result">结果,可为空</param>
        /// <param name="span"></param>

        public CommandExecutedEventArgs(string fileName, string args, ICommandResult result, TimeSpan span)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            FileName = fileName;
            Args = args;
            UsedTime = span;
        }
    }
    /// <summary>
    /// 命令执行中事件参数
    /// </summary>
    public class CommandExecutingEventArgs : EventArgs
    {
        /// <summary>
        /// 命令文件名
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// 参数
        /// </summary>
        public string[] Args { get; }
        /// <summary>
        /// 构造一个命令执行中事件参数
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        public CommandExecutingEventArgs(string fileName, params string[] args)
        {
            FileName = fileName;
            Args = args;
        }
    }
}
