/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 12:12:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Diagnostics;
using System.Linq;
namespace AutumnBox.Support.Log
{
    public static partial class Logger
    {
        public static void Debug(object senderOrTag,object message) {
            var str = MakeText(senderOrTag, "Debug", message.ToString());
            Debugger.Log(4,null,str);
        }
        public static void Info(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Info", message.ToString());
            Debugger.Log(3, null, str);
        }
        public static void Warn(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Warn", message.ToString());
            Debugger.Log(2, null, str);
        }
        public static void Fatal(object senderOrTag, object message)
        {
            var str = MakeText(senderOrTag, "Fatal", message.ToString());
            Debugger.Log(1, null, str);
        }
        private static void WriteToFile() { }
    }
}
