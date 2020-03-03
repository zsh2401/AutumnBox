using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Diagnostics;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class OSImpl : IOperatingSystemAPI
    {
        public bool IsRunAsAdmin => OpenFxLoader.BaseApi.IsRunAsAdmin;

        public bool IsWindows10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
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
                SLogger<OSImpl>.Warn("failed", ex);
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
                    exitCode = proc.ExitCode;
                    proc.Dispose();
                    return exitCode == 0;
                }
            }
            catch (Exception ex)
            {
                SLogger<OSImpl>.Warn("failed", ex);
                return false;
            }
        }
    }
}
