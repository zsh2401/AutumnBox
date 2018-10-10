/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 2:15:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Util.Debugging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// Process拓展
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// 判断是否在运行
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 使用Windows taskkill命令进行结束
        /// </summary>
        /// <param name="proc"></param>
        /// <exception cref="PlatformNotSupportedException">此命令仅支持WIN32平台,在其它平台调用将抛出此异常</exception>
        public static void KillByTaskKill(this Process proc)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var logger = new Logger(nameof(ProcessExtensions));
                new WindowsCmdCommand($"taskkill /F /PID {proc.Id}")
                    .To((e) =>
                    {
                        logger.Info(e.Text);
                    })
                    .Execute();
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
        /// <summary>
        /// Kill a process, and all of its children, grandchildren, etc.
        /// </summary>
        /// <param name="pid">Process ID.</param>
        public static void KillProcessAndChildren(int pid)
        {
            // Cannot close 'system idle process'.
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
    }
}
