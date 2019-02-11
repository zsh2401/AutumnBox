/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:59:09 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.OS;
using AutumnBox.Logging;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.GUI.Util
{
    static class AlreadyHaveAutumnBoxChecker
    {
        private const int SW_SHOWNOMAL = 1;
        private const string TAG = nameof(AlreadyHaveAutumnBoxChecker);
        private static readonly ILogger logger = LoggerFactory.Auto(nameof(AlreadyHaveAutumnBoxChecker));
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
            var process = FindOtherProcess();
            if (process != null)
            {
                NativeMethods.ShowWindowAsync(process.MainWindowHandle, SW_SHOWNOMAL);
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
            }
            return process == null;
        }
    }
}
