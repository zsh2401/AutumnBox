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
    public interface ICompletable:IForceStoppable
    {
        event EventHandler NoArgFinished;
    }
}
