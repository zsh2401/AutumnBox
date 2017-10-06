/* =============================================================================*\
*
* Filename: Logger.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:41:09(UTC+8:00)
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
namespace AutumnBox.Util
{
    public static class Logger
    {
        public static readonly string LOG_FILE = "autumnbox.log";
        public static void InitLogFile()
        {
#if DELETE_LAST_LOG
            File.Delete(LogPath);
            File.Create(LogPath);
#endif
        }
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
            else
            {
                t = tag.GetType().Name;
            }
            string m = SharedTools.Logger.ToFullMessage(t, message);
            Trace.WriteLine(m);
            SharedTools.Logger.WriteToFile(LOG_FILE, m);
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
