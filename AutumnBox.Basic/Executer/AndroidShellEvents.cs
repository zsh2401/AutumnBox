/* =============================================================================*\
*
* Filename: AndroidShellEvents
* Description: 
*
* Version: 1.0
* Created: 2017/11/21 17:16:11 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 输入事件处理器委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void InputReceivedEventHandler(object sender, InputReceivedEventArgs e);
    /// <summary>
    /// 输入事件处理参数
    /// </summary>
    public class InputReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 输入的命令
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 输入的时间
        /// </summary>
        public readonly DateTime Time;
        /// <summary>
        /// 构造
        /// </summary>
        public InputReceivedEventArgs()
        {
            Time = DateTime.Now;
        }
    }
}
