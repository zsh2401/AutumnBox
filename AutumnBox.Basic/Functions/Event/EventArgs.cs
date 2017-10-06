/* =============================================================================*\
*
* Filename: EventArgs.cs
* Description: 
*
* Version: 1.0
* Created: 9/16/2017 15:16:14(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Functions.Event
{
    using AutumnBox.Basic.Executer;
    using System;
    /// <summary>
    /// 功能完成执行的事件参数
    /// </summary>
    public class FinishEventArgs : EventArgs
    {
        public OutputData OutputData { get { return Result.OutputData; } }
        public ExecuteResult Result { get; internal set; }
    }
    /// <summary>
    /// 功能模块开始执行时的事件参数
    /// </summary>
    public class StartEventArgs : EventArgs{}
}
