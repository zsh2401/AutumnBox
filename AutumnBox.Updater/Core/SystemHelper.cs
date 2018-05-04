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
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;

namespace AutumnBox.Updater.Core
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
            }

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }
        }
    }
}
