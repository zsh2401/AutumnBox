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

namespace AutumnBox.Shared.CstmDebug
{
    public static class Logger
    {
        private static readonly string DEFAULT_LOGFLODER = "logs/";
        private static readonly string DEFAULT_LOGFILE = "default.log";
        private static readonly string NewFloder;
        static Logger()
        {
            if (!Directory.Exists(DEFAULT_LOGFLODER)) Directory.CreateDirectory(DEFAULT_LOGFLODER);
            NewFloder = DateTime.Now.ToString("yy_MM_dd/");
            if (!Directory.Exists(DEFAULT_LOGFLODER + NewFloder)) Directory.CreateDirectory(DEFAULT_LOGFLODER + NewFloder);
        }
        public static void D(object sender, string message, bool IsError = false)
        {
            if (!GetShowProp(sender)) return;
            string full = ToFullMessage(sender, message, IsError);
            Debug.WriteLine(full);
            WriteToFile(sender, full);
        }
        public static void D(object sender, string message, Exception e)
        {
            if (!GetShowProp(sender)) return;
            D(sender, message, true);
            D(sender, e.ToString() + e.Message, true);
        }
        public static void T(object sender, string message, bool IsError = false)
        {
            if (!GetShowProp(sender)) return;
            string full = ToFullMessage(sender, message, IsError);
            Trace.WriteLine(full);
            WriteToFile(sender, full);
        }
        public static void T(object sender, string message, Exception e)
        {
            if (!GetShowProp(sender)) return;
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
        private static bool GetShowProp(object sender)
        {
            try
            {
                return ((LogPropertyAttribute)
                    Attribute.GetCustomAttribute(sender.GetType(), typeof(LogPropertyAttribute))).Show;
            }
            catch
            {
                return true;
            }
        }
        private static string SenderToTag(object sender)
        {
            try
            {
                string tag = ((LogPropertyAttribute)
                    Attribute.GetCustomAttribute(sender.GetType(), typeof(LogPropertyAttribute))).TAG ?? throw new NullReferenceException();
                if (tag != LogPropertyAttribute.NOT_LOAD_TAG)
                {
                    return tag;
                }
            }
            catch { }
            if (sender is string) return sender.ToString();
            else return sender.GetType().Name;
        }
        private static void WriteToFile(object sender, string fullMessage)
        {
            string _LogFileName = DEFAULT_LOGFILE;
            var attrs = System.Reflection.Assembly.GetAssembly(sender.GetType()).
                GetCustomAttributes(typeof(LogFilePropertyAttribute), true);
            if (attrs.Length != 0)
            {
                _LogFileName = ((LogFilePropertyAttribute)attrs[attrs.Length - 1]).FileName;
            }
            try
            {
                StreamWriter sw = new StreamWriter(DEFAULT_LOGFLODER + NewFloder + _LogFileName, true);
                sw.WriteLine(fullMessage);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }
    }
}
