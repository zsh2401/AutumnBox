/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:59:09 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.OS;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    static class AlreadyHaveAutumnBoxChecker
    {
        private const int SW_SHOWNOMAL = 1;
        private const string TAG = nameof(AlreadyHaveAutumnBoxChecker);
        private static Process FindOtherProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            Logger.Info(TAG, $"have {Processes.Count()} autumnbox.gui process");
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    return process;
                    //if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    //{
                    //    return process;
                    //}
                }
            }
            return null;
        }
        public static void Do()
        {
            var process = FindOtherProcess();
            if (process != null)
            {
                Logger.Debug(TAG,"found other autumnbox process,switch to it,and shutdown current");
                Logger.Debug(TAG, "ShowWindowAsync()");
                NativeMethods.ShowWindowAsync(process.MainWindowHandle, SW_SHOWNOMAL);
                Logger.Debug(TAG, "SetForegroundWindow()");
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                App.Current.Dispatcher.Invoke(()=> {
                    App.Current.Shutdown(1);
                });
            }
        }
    }
}
