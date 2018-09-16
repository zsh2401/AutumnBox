/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:59:09 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.OS;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.GUI.Util
{
    static class AlreadyHaveAutumnBoxChecker
    {
        private const int SW_SHOWNOMAL = 1;
        private const string TAG = nameof(AlreadyHaveAutumnBoxChecker);
        private static readonly ILogger logger = new Logger(nameof(AlreadyHaveAutumnBoxChecker));
        private static Process FindOtherProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            logger.Info($"have {Processes.Count()} autumnbox.gui process");
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
                logger.Debug("found other autumnbox process,switch to it,and shutdown current");
                logger.Debug("ShowWindowAsync()");
                NativeMethods.ShowWindowAsync(process.MainWindowHandle, SW_SHOWNOMAL);
                logger.Debug("SetForegroundWindow()");
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                App.Current.Dispatcher.Invoke(() =>
                {
                    App.Current.Shutdown(1);
                });
            }
        }
    }
}
