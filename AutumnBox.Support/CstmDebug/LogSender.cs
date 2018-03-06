/* =============================================================================*\
*
* Filename: LogSender
* Description: 
*
* Version: 1.0
* Created: 2017/11/1 21:20:59 (UTC+8:00)
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
    public sealed class LogSender : ILogSender
    {
        public string LogTag { get; private set; }
        public bool IsShowLog { get; private set; }
        public object Owner { get; private set; }
        public LogSender(object owner, string tag = null, bool show = true)
        {
            Owner = owner;
            LogTag = tag ?? owner.GetType().Name;
            IsShowLog = show;
        }
        public Type GetOwnerType()
        {
            return Owner.GetType();
        }
    }
}
