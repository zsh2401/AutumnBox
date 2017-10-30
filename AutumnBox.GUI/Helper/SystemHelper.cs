/* =============================================================================*\
*
* Filename: SystemHelper.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 05:04:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.GUI.Util;
using AutumnBox.Shared.CstmDebug;
using System;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.GUI.Helper
{
    /// <summary>
    /// SystemHelper static class, static methods about System
    /// </summary>
    public static class SystemHelper
    {
        public static DateTime CompiledDate
        {
            get
            {
                var o = (CompiledDateAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(), typeof(CompiledDateAttribute));
                return o.DateTime;
            }
        }
        public static Version NowVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
        public static void KillProcess(string processName)
        {
            var list = Process.GetProcessesByName(processName);
            foreach (Process p in list)
            {
                p.Kill();
            }
        }
        public static bool IsWin10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
        public static void AppExit(int exitCode = 0)
        {
            Logger.T("Exiting.....");
            App.DevicesListener.Stop();
            CommandExecuter.Kill();
            Environment.Exit(exitCode);
        }
        internal readonly static AutoGCer GCer = new AutoGCer();
    }
}
