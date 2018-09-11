/* =============================================================================*\
*
* Filename: SystemHelper
* Description: 
*
* Version: 1.0
* Created: 2017/10/14 1:22:46(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
#if !LINUX
using System.Management;
#endif
using System.Text;
namespace AutumnBox.Support.Helper
{
    public static class SystemHelper
    {
        /// <summary>
        /// 杀死进程和他的子进程
        /// Code from https://stackoverflow.com/questions/30249873/process-kill-doesnt-seem-to-kill-the-process
        /// </summary>
        /// <param name="pid"></param>
        public static void KillProcessAndChildrens(int pid)
        {
#if !LINUX
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();
            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
            catch (Win32Exception e)
            {
                Logger.Warn("Unknow exception", e);
            }

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }
#else
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                throw new NotImplementedException();
            }
#endif
        }
    }
}
