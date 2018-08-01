/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/11 12:29:34 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Diagnostics;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 操作系统API
    /// </summary>
    public static class OS
    {
        /// <summary>
        /// 判断是否是WIN10系统
        /// </summary>
        public static bool IsWindows10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
        /// <summary>
        /// 安装驱动
        /// </summary>
        /// <param name="ctx">调用者</param>
        /// <param name="infFilePath">驱动inf路径</param>
        /// <returns></returns>
        public static bool InstallDriver(Context ctx, string infFilePath)
        {
            return InstallUsePnPUtil(infFilePath) | InstallUsePnPUtil_SysNative(infFilePath);
        }


        #region PRIVATE
        private class OSLogSender : Context { }
        private static readonly OSLogSender ctx = new OSLogSender();
        private static bool InstallUsePnPUtil_SysNative(string fileName)
        {
            try
            {
                var winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                var o = CommandExecuter.Static.Execute($"{winDir}\\sysnative\\pnputil", $"-i -a {fileName}");
                return o.GetExitCode() == 0;
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(ctx, "failed", ex);
                return false;
            }
        }
        private static bool InstallUsePnPUtil(string fileName)
        {
            try
            {
                var startInfo = new ProcessStartInfo("pnputil", $"-i -a {fileName}")
                {
                    Verb = "runas",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };
                var outputBuilder = new AdvanceOutputBuilder();
                using (var proc = Process.Start(startInfo))
                {
                    proc.OutputDataReceived += (s, e) => { outputBuilder.AppendOut(e.Data); };
                    proc.ErrorDataReceived += (s, e) => { outputBuilder.AppendError(e.Data); };
                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();
                    outputBuilder.Result.PrintOnLog(ctx);
                    return proc.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(ctx, "failed", ex);
                return false;
            }
        }
        #endregion
    }
}
