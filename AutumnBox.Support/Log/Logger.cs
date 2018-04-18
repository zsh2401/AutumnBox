/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 12:12:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Support.Log
{
    public class LogEventArgs : EventArgs
    {
        public string FullMessage { get; private set; }
        public LogEventArgs(string msg)
        {
            FullMessage = msg;
        }
    }
    public static partial class Logger
    {
        public static event EventHandler<LogEventArgs> Logged;

        public readonly static StringBuilder logBuffer = new StringBuilder();

        [Conditional("DEBUG")]
        public static void Debug(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Debug", message);
            Debugger.Log(4, null, str);
            logBuffer.Append(str);
            WriteToFile(str);
        }
        [Conditional("DEBUG")]
        public static void DebugWarn(object senderOrTag, object message,Exception ex=null) {
            var str = MakeText(senderOrTag, "Debug-Warn", message,ex);
            Debugger.Log(4, null, str);
            logBuffer.Append(str);
            WriteToFile(str);
        }

        public static void Info(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Info", message);
            Debugger.Log(3, null, str);
            logBuffer.Append(str);
            WriteToFile(str);
        }

        public static void Warn(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Warn", message);
            Debugger.Log(2, null, str);
            logBuffer.Append(str);
            WriteToFile(str);
        }

        public static void Warn(object senderOrTag, object message, Exception e)
        {
            var str = MakeText(senderOrTag, "Warn", message, e);
            Debugger.Log(2, null, str);
            logBuffer.Append(str);
            WriteToFile(str);
        }

        public static void Fatal(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Fatal", message);
            Debugger.Log(1, null, str);
            logBuffer.Append(str);
            WriteToFile(str);
        }
    }
}
