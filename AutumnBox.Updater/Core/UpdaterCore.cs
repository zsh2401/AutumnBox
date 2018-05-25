/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 17:45:45 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Updater.Core.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    static class UpdaterCore
    {
        public const string UPDATE_API = "http://atmb.top/api/update_dv";
        public static readonly IUpdater Updater;
        static UpdaterCore() {
            Updater = new Npdater();
        }
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
