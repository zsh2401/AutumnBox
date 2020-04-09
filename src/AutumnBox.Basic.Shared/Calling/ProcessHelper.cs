using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// Calling命名空间中一些进程相关的通用辅助函数
    /// </summary>
    public static class ProcessHelper
    {
        public static ProcessStartInfo GetCmdStartInfo(string fileName, string args)
        {
            var pInfo = new ProcessStartInfo()
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = fileName,
                Arguments = args
            };
            return pInfo;
        }
        public static void TaskKill(int pid) { }
    }
}
