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

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// ADB帮助(管理)
    /// </summary>
    public static class AdbHelper
    {
        /// <summary>
        /// 当ADB服务器失败时触发
        /// </summary>
        public static event EventHandler AdbServerStartsFailed;
        /// <summary>
        /// 触发adb服务失败事件
        /// </summary>
        internal static void RisesAdbServerStartsFailedEvent()
        {
            AdbServerStartsFailed?.Invoke(new object(), new EventArgs());
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
        /// 启动ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool StartServer()
        {
            var result = CommandExecuter.Static.Execute(AdbConstants.FullAdbFileName, "start-server");
            bool successful = result.IsSuccessful && !result.Contains("error");
            result.PrintOnLog(nameof(AdbHelper), true);
            if (!successful) RisesAdbServerStartsFailedEvent();
            return successful;
        }
        /// <summary>
        /// 关闭ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool StopServer()
        {
            var result = CommandExecuter.Static.Execute(AdbConstants.FullAdbFileName, "kill-server");
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
