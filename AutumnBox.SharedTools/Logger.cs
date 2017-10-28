/* =============================================================================*\
*
* Filename: Logger
* Description: 
*
* Version: 1.0
* Created: 2017/10/28 15:35:06 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutumnBox.Shared
{
    public static class Logger
    {
        private static readonly string DEFAULT_LOGFLODER = "logs/";
        private static readonly string DEFAULT_LOGFILE = "default.log";
        private static readonly string Prefix;
        static Logger()
        {
            if (!Directory.Exists(DEFAULT_LOGFLODER)) Directory.CreateDirectory(DEFAULT_LOGFLODER);
            Prefix = DateTime.Now.ToString("mm_ss_");
        }
        public static void D(object sender, string message, bool IsError = false)
        {
            string full = ToFullMessage(sender, message, IsError);
            Debug.WriteLine(full);
            WriteToFile(sender, full);
        }
        public static void D(object sender, string message, Exception e)
        {
            D(sender, message, true);
            D(sender, e.ToString() + e.Message, true);
        }
        public static void T(object sender, string message, bool IsError = false)
        {
            string full = ToFullMessage(sender, message, IsError);
            Trace.WriteLine(full);
            WriteToFile(sender, full);
        }
        public static void T(object sender, string message, Exception e)
        {
            T(sender, message, true);
            T(sender, e.ToString() + e.Message, true);
        }

        private static string ToFullMessage(object sender, string message, bool IsError = false)
        {
            string t = $"[{DateTime.Now.ToString("yy-MM-dd_hh:mm:ss")}]";
            if (IsError)
            {
                return $"{t} [{ SenderToTag(sender)}/WARNING]  : {message}";
            }
            else
            {
                return $"{t} [{ SenderToTag(sender)}/INFO]  : {message}";
            }
        }
        private static string SenderToTag(object sender)
        {
            try
            {
                LogSenderPropAttribute attr;
                var attrs = sender.GetType().GetCustomAttributes(typeof(LogSenderPropAttribute), true);
                int length = attrs.Length;
                attr = (LogSenderPropAttribute)attrs[length - 1];
                return attr.TAG;
            }
            catch { }
            if (sender is string) return sender.ToString();
            else return sender.GetType().Name;
        }
        private static void WriteToFile(object sender, string fullMessage)
        {
            string _LogFileName = DEFAULT_LOGFILE;
            var attrs = System.Reflection.Assembly.GetAssembly(sender.GetType()).
                GetCustomAttributes(typeof(LogFilePropAttribute), true);
            if (attrs.Length != 0)
            {
                _LogFileName = ((LogFilePropAttribute)attrs[attrs.Length - 1]).FileName;
            }
            try
            {
                StreamWriter sw = new StreamWriter(DEFAULT_LOGFLODER + Prefix + _LogFileName, true);
                sw.WriteLine(fullMessage);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }
    }
}
