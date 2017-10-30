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
        public static void D(string message, bool isError = false)
        {
            string full = GetFullMessage(GetTag(), message, isError);
            Debug.WriteLine(full);
            WriteToFile(full);
        }
        public static void D(string message, Exception e)
        {
            StringBuilder full = new StringBuilder(GetFullMessage(GetTag(), message, true));
            full.Append(e.ToString() + e.Message);
            Debug.WriteLine(full.ToString());
            WriteToFile(full.ToString());
        }
        public static void T(string message, bool isError = false)
        {
            string full = GetFullMessage(GetTag(), message, isError);
            Trace.WriteLine(full);
            WriteToFile(full);
        }
        public static void T(string message, Exception e)
        {
            StringBuilder full = new StringBuilder(GetFullMessage(GetTag(), message, true));
            full.Append(e.ToString() + e.Message);
            Trace.WriteLine(full.ToString());
            WriteToFile(full.ToString());
        }
        private static string GetTag()
        {
            var calledMethod = new StackTrace().GetFrames()[2].GetMethod();
            //Try to Get method TAG
            var methodAttrs = calledMethod.GetCustomAttributes(false);
            foreach (var attr in methodAttrs)
            {
                if (attr is LogPropertyAttribute) { return ((LogPropertyAttribute)attr).TAG; }
            }
            //Try to Get class TAG
            var classAttrs = calledMethod.ReflectedType.GetCustomAttributes(true);
            foreach (var attr in classAttrs)
            {
                if (attr is LogPropertyAttribute) { return ((LogPropertyAttribute)attr).TAG; }
            }
            //WTF
            return calledMethod.ReflectedType.Name;
        }
        private static string GetFullMessage(string tag, string message, bool isError)
        {
            string t = $"[{DateTime.Now.ToString("yy-MM-dd_hh:mm:ss")}]";
            if (isError)
            {
                return $"{t} [{ tag}/WARNING]  : {message}";
            }
            else
            {
                return $"{t} [{ tag}/INFO]  : {message}";
            }
        }
        private static void WriteToFile(string fullMsg)
        {
            string _LogFileName = DEFAULT_LOGFILE;
            var assInfo = System.Reflection.Assembly.GetAssembly(new StackTrace().GetFrames()[2].GetMethod().ReflectedType);
            var assAttr = assInfo.GetCustomAttributes(typeof(LogFilePropertyAttribute), true);
            if (assAttr.Length != 0)
            {
                _LogFileName = ((LogFilePropertyAttribute)assAttr[0]).FileName;
            }
            try
            {
                StreamWriter sw = new StreamWriter(DEFAULT_LOGFLODER + NewFloder + _LogFileName, true);
                sw.WriteLine(fullMsg);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }

        #region Obsolete
        [Obsolete]
        public static void D(object sender, string message, bool IsError = false)
        {
            if (!GetShowProp(sender)) return;
            string full = ToFullMessage(sender, message, IsError);
            Debug.WriteLine(full);
            WriteToFile(sender, full);
        }
        [Obsolete]
        public static void D(object sender, string message, Exception e)
        {
            if (!GetShowProp(sender)) return;
            D(sender, message, true);
            D(sender, e.ToString() + e.Message, true);
        }
        [Obsolete]
        public static void T(object sender, string message, bool IsError = false)
        {
            if (!GetShowProp(sender)) return;
            string full = ToFullMessage(sender, message, IsError);
            Trace.WriteLine(full);
            WriteToFile(sender, full);
        }
        [Obsolete]
        public static void T(object sender, string message, Exception e)
        {
            if (!GetShowProp(sender)) return;
            T(sender, message, true);
            T(sender, e.ToString() + e.Message, true);
        }
        [Obsolete]
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
        [Obsolete]
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
        [Obsolete]
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
        [Obsolete]
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
            catch (Exception e) { D("Fucked exp", e); }
        }
        #endregion
    }
}
