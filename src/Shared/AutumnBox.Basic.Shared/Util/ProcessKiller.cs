/*

* ==============================================================================
*
* Filename: ProcessKiller
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 20:31:50
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#if WIN32
using System.Management;
#endif

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 进程杀死器
    /// </summary>
    public static class ProcessKiller
    {
        /// <summary>
        /// 尽可能干净地清理一个进程
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">平台不支持</exception>
        /// <param name="pid"></param>
        public static void FKill(int pid)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    UseWin32(pid);
                    break;
                case PlatformID.Unix:
                    UseUnix(pid);
                    break;
                default:
                    throw new PlatformNotSupportedException();
            }
        }
        private static void UseWin32(int pid)
        {
#if WIN32
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
                ManagementObjectCollection moc = searcher.Get();
                foreach (ManagementObject mo in moc)
                {
                    UseWin32(Convert.ToInt32(mo["ProcessID"]));
                }
                Process.GetProcessById(pid).Kill();
            }
            catch
            {
                /* process already exited */
            }
#else
            Process.GetProcessById(pid).Kill();
#endif
        }
        private static void UseUnix(int pid) { }
    }
}
