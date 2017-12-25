/* =============================================================================*\
*
* Filename: ILogSender
* Description: 
*
* Version: 1.0
* Created: 2017/11/1 21:39:00 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Support.CstmDebug
{
    public interface ILogSender
    {
        string LogTag { get; }
        bool IsShowLog { get; }
    }
}
