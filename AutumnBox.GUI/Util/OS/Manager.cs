using AutumnBox.GUI.Util.Debugging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;

namespace AutumnBox.GUI.Util.OS
{
    internal static class Manager
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
                SLogger.Warn("Unknow exception", e);
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
