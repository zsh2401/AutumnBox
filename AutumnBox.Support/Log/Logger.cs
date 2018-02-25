/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 12:12:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
namespace AutumnBox.Support.Log
{
    public static partial class Logger
    {
        public static void Debug(object senderOrTag,object message) {
            var str = MakeText(senderOrTag, "Debug", message);
            Debugger.Log(4,null,str);
            WriteToFile(str);
        }
        public static void Info(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Info", message);
            Debugger.Log(3, null, str);
            WriteToFile(str);
        }
        public static void Warn(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Warn", message);
            Debugger.Log(2, null, str);
            WriteToFile(str);
        }
        public static void Fatal(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Fatal", message);
            Debugger.Log(1, null, str);
            WriteToFile(str);
        }

    }
}
