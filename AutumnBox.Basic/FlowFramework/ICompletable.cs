/* =============================================================================*\
*
* Filename: ICompletable
* Description: 
*
* Version: 1.0
* Created: 2017/12/5 1:17:24 (UTC+8:00)
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
    /// 实现这个结构的任何东西将可以通知结束运行以及被结束运行的
    /// </summary>
    public interface ICompletable:IForceStoppable
    {
        /// <summary>
        /// 仅仅只有通知功能流程已结束一个功能的事件
        /// </summary>
        event EventHandler NoArgFinished;
    }
}
