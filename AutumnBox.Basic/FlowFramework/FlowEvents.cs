/* =============================================================================*\
*
* Filename: FlowEvents
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:15:10 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

using System;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 功能流程结束时的事件处理器函数
    /// </summary>
    /// <typeparam name="TResult">泛型的结果</typeparam>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">参数</param>
    public delegate void FinishedEventHandler<TResult>(object sender, FinishedEventArgs<TResult> e) where TResult : FlowResult;
    /// <summary>
    /// 功能流程结束时的事件处理器函数的参数
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public sealed class FinishedEventArgs<TResult> : EventArgs where TResult : FlowResult
    {
        /// <summary>
        /// 泛型的结果
        /// </summary>
        public TResult Result { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="result"></param>
        public FinishedEventArgs(TResult result)
        {
            Result = result;
        }
    }
    /// <summary>
    /// 当功能流程启动时触发的事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void StartupEventHandler(object sender, StartupEventArgs e);
    /// <summary>
    /// 当功能流程启动时触发的事件委托的参数
    /// </summary>
    public class StartupEventArgs : EventArgs { }
}
