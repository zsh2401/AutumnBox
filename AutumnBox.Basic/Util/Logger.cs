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
    public static class Logger
    {
        public static readonly string LOG_FILE = "basic.log";
        /// <summary>
        /// DEBUG状态下打印信息
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void D(object tag, string message)
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
        public static void T(object tag, string message)
        {
            string t;
            if (tag is string)
            {
                t = tag.ToString();
            }
            else {
                t = tag.GetType().Name;
            }
            string m = SharedTools.Logger.ToFullMessage(t,message);
            Trace.WriteLine(m);
            SharedTools.Logger.WriteToFile(LOG_FILE,m);
        }
        /// <summary>
        /// RELEASE和DEBUG下打印错误
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void E(object tag, string message, Exception e, bool showInRelease = true)
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
    }
}
