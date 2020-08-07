/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:59:09 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Logging;
using System.Diagnostics;

namespace AutumnBox.GUI.Services.Impl.OS
{
    static class OtherProcessChecker
    {
        private const int SW_SHOWNOMAL = 1;
        private const string TAG = nameof(OtherProcessChecker);
        private static readonly ILogger logger = LoggerFactory.Auto(nameof(OtherProcessChecker));
        private static Process FindOtherProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    return process;
                }
            }
            return null;
        }
        public static bool Do()
        {
            var process = ThereIsOtherAutumnBoxProcess();
            if (process != null)
            {
                NativeMethods.ShowWindowAsync(process.MainWindowHandle, SW_SHOWNOMAL);
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
            }
            return process == null;
        }
        public static Process ThereIsOtherAutumnBoxProcess()
        {
            return FindOtherProcess();
        }
    }
}
