/*
  Logger打印器
 @zsh2401
 2017/9/8
 */
using System;
using System.Diagnostics;
using System.IO;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    internal static class Logger
    {
        //private static readonly string TAG = "Logger";
        public static readonly string LOG_FILE = "basic.log";
        /// <summary>
        /// DEBUG状态下打印信息
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void D(string tag, string message)
        {
#if DEBUG
            T(tag, message);
#endif
        }
        /// <summary>
        /// RELEASE和DEBUG都打印信息
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void T(string tag, string message)
        {
            string m = $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] { tag} : {message}";
            Trace.WriteLine(m);
            WriteToLogFile(m);
        }
        /// <summary>
        /// RELEASE和DEBUG下打印错误
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void E(string tag, string message, Exception e, bool showInRelease = true)
        {
            if (showInRelease)
            {
                T(tag, message);
                T(e.ToString(), e.Message);
            }
            else
            {
                D(tag, message);
                D(e.ToString(), e.Message);
            }
        }
        /// <summary>
        /// 将传入的信息写到log文件(增添的方式)
        /// </summary>
        /// <param name="content"></param>
        static void WriteToLogFile(string content)
        {
            try
            {
                StreamWriter sw = new StreamWriter(LOG_FILE, true);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }
    }
}
