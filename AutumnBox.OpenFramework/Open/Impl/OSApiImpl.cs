/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 21:19:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.OpenFramework.Content;
using System;
using System.Diagnostics;
using System.Security.Principal;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class OSApiImpl : Context, IOSApi
    {
        public bool IsWindows10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
        public override string LoggingTag => "AutumnBox OperatingSystemAPI";

        public bool IsRunAsAdmin
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public bool InstallDriver(string infFilePath)
        {
            return InstallUsePnPUtil(infFilePath) | InstallUsePnPUtil_SysNative(infFilePath);
        }

        private bool InstallUsePnPUtil_SysNative(string fileName)
        {
            try
            {
                var winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                var result = new ProcessBasedCommand($"{winDir}\\sysnative\\pnputil", $"-i -a {fileName}").Execute();
                return result.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Logger.Warn("failed", ex);
                return false;
            }
        }

        private bool InstallUsePnPUtil(string fileName)
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
                var outputBuilder = new OutputBuilder();
                int exitCode = 0;
                using (var proc = Process.Start(startInfo))
                {
                    proc.OutputDataReceived += (s, e) => { outputBuilder.AppendOut(e.Data); };
                    proc.ErrorDataReceived += (s, e) => { outputBuilder.AppendError(e.Data); };
                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();
                    Logger.Info(outputBuilder.Result.ToString());
                    exitCode = proc.ExitCode;
                    proc.Dispose();
                    return exitCode == 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("failed", ex);
                return false;
            }
        }
    }
}
