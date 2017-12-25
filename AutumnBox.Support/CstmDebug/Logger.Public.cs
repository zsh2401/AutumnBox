/* =============================================================================*\
*
* Filename: Logger.Public
* Description: 
*
* Version: 1.0
* Created: 2017/11/1 22:55:43 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.CstmDebug
{
    partial class Logger
    {
        public static void D(string message, bool isWarning = false)
        {
#if !DEBUG
            return;
#endif
            var methodCaller = GetCaller();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(methodCaller);
            if (!attrInfo.Show) return;
            string full = GetFullMessage(attrInfo.TAG, message, isWarning.ToErrorLevel());
            Debug.WriteLine(full);
            WriteToFile(LogFileNameOf(methodCaller), full);
        }
        public static void D(string message, Exception e)
        {
#if !DEBUG
            return;
#endif
            var methodCaller = GetCaller();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(methodCaller);
            if (!attrInfo.Show) return;
            StringBuilder full = new StringBuilder(GetFullMessage(attrInfo.TAG, message, 2));
            full.Append(Environment.NewLine + GetFullMessage(attrInfo.TAG, e.ToString() + e.Message, 2));
            Debug.WriteLine(full.ToString());
            WriteToFile(LogFileNameOf(methodCaller), full.ToString());
        }
        public static void T(string message, bool isWarning = false)
        {
            var methodCaller = GetCaller();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(methodCaller);
            if (!attrInfo.Show) return;
            string full = GetFullMessage(attrInfo.TAG, message, isWarning.ToErrorLevel());
            Trace.WriteLine(full);
            WriteToFile(LogFileNameOf(methodCaller), full);
        }
        public static void T(string message, Exception e)
        {
            var methodCaller = GetCaller();
            LogPropertyAttribute attrInfo = GetLogPropertyAttribute(methodCaller);
            if (!attrInfo.Show) return;
            StringBuilder full = new StringBuilder(GetFullMessage(attrInfo.TAG, message, 2));
            full.Append(Environment.NewLine + GetFullMessage(attrInfo.TAG, e.ToString() + e.Message, 2));
            Trace.WriteLine(full.ToString());
            WriteToFile(LogFileNameOf(methodCaller), full.ToString());
        }
        public static void D(ILogSender sender, string message, bool isWarning = false)
        {
            if (!(sender.IsShowLog)) return;
            string full = GetFullMessage(sender.LogTag, message, isWarning.ToErrorLevel());
            Debug.WriteLine(full);
            WriteToFile(LogFileNameOf(sender), full);
        }
        public static void D(ILogSender sender, string message, Exception e)
        {
            if (!(sender.IsShowLog)) return;
            StringBuilder full = new StringBuilder(GetFullMessage(sender.LogTag, message, 2));
            full.Append(Environment.NewLine + GetFullMessage(sender.LogTag, e.ToString() + e.Message, 2));
            Debug.WriteLine(full.ToString());
            WriteToFile(LogFileNameOf(sender), full.ToString());
        }
        public static void T(ILogSender sender, string message, bool IsWarning = false)
        {
            if (!(sender.IsShowLog)) return;
            string full = GetFullMessage(sender.LogTag, message, IsWarning.ToErrorLevel());
            Trace.WriteLine(full);
            WriteToFile(LogFileNameOf(sender), full);
        }
        public static void T(ILogSender sender, string message, Exception e)
        {
            if (!(sender.IsShowLog)) return;
            StringBuilder full = new StringBuilder(GetFullMessage(sender.LogTag, message, 2));
            full.Append(Environment.NewLine + GetFullMessage(sender.LogTag, e.ToString() + e.Message, 2));
            Trace.WriteLine(full.ToString());
            WriteToFile(LogFileNameOf(sender), full.ToString());
        }
    }
}
