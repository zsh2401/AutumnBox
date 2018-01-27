/* =============================================================================*\
*
* Filename: AdbHelper
* Description: 
*
* Version: 1.0
* Created: 2017/11/21 0:03:28 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using AutumnBox.Support.Helper;
using System;
using System.Diagnostics;

namespace AutumnBox.Basic.Adb
{
    public static class AdbHelper
    {
        public static event EventHandler AdbServerStartsFailed;
        public static event EventHandler AdbServerStopsFailed;
        /// <summary>
        /// 判断是否已有别的ADB进程
        /// </summary>
        /// <returns></returns>
        public static bool AlreadyHaveAdbProcess()
        {
            return Process.GetProcessesByName("adb").Length != 0;
        }
        /// <summary>
        /// 杀死所有ADB进程
        /// </summary>
        public static void KillAllAdbProcess()
        {
            Process[] processs = Process.GetProcessesByName("adb");
            foreach (var p in processs)
            {
                SystemHelper.KillProcessAndChildrens(p.Id);
            }
        }
        /// <summary>
        /// 检查ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool Check()
        {
            if (!AlreadyHaveAdbProcess())
            {
                return StartServer();
            }
            return true;
        }
        /// <summary>
        /// 启动ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool StartServer()
        {
            var result = new CommandExecuter().Execute(AdbConstants.FullAdbFileName, "start-server");
            bool successful = result.IsSuccessful && !result.Output.All.ToString().Contains("cannot connect to daemon");
            if (!successful) AdbServerStartsFailed?.Invoke(new object(), new EventArgs());
            return successful;
        }
        /// <summary>
        /// 关闭ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool StopServer()
        {
            var result = new CommandExecuter().Execute(AdbConstants.FullAdbFileName, "start-server");
            if (!result.IsSuccessful) AdbServerStopsFailed?.Invoke(new object(), new EventArgs());
            return result.IsSuccessful;
        }
        /// <summary>
        /// 重启ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool RestartServer()
        {
            StopServer();
            return StartServer();
        }
    }
}
