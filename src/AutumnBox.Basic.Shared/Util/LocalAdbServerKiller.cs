/*

* ==============================================================================
*
* Filename: AdbServerKiller
* Description: 
*
* Version: 1.0
* Created: 2020/8/14 0:23:38
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    internal sealed class LocalAdbServerKiller
    {
        private const int MAX_TIMEOUT = 3000;
        private readonly ICommandProcedure commandKillServer;
        private readonly ILogger logger;
        public LocalAdbServerKiller(ICommandProcedure killServerCommand)
        {
            logger = LoggerFactory.Auto<LocalAdbServerKiller>();
            commandKillServer = killServerCommand ?? throw new ArgumentNullException(nameof(killServerCommand));
        }

        public Task Kill()
        {
            return Task.Run(() =>
            {
                commandKillServer.OutputReceived += (s, e) =>
                {
                    SLogger.Info(this, $"{e.Text}");
                };
                commandKillServer.ExecuteAsync();
                Thread.Sleep(MAX_TIMEOUT);
                if (commandKillServer.Status != CommandStatus.Executing && commandKillServer.Result.ExitCode == 0)
                {
                    commandKillServer.Cancel();
                    commandKillServer.Dispose();
                    KilProcesses();
                }
            });
        }

        private void KilProcesses()
        {
            var psi = new ProcessStartInfo()
            {
                FileName = "taskkill",
                Arguments = "/f /im adb.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            var proc = Process.Start(psi);
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.OutputDataReceived += (s, e) => SLogger<LocalAdbServerKiller>.Info(e.Data);
            proc.ErrorDataReceived += (s, e) => SLogger<LocalAdbServerKiller>.Warn(e.Data);
            //proc.WaitForExit();
            Thread.Sleep(MAX_TIMEOUT);
            if (proc.HasExited)
            {
                if (proc.ExitCode != 0)
                {
                    logger.Warn("The command taskkil looks like not working. Exit code is " + proc.ExitCode);
                }
                logger.Info("The command taskkil has successfully executed.");
            }
            else
            {
                logger.Warn("The command taskkil looks like not working.");
            }
            proc.CancelOutputRead();
            proc.CancelErrorRead();
            proc.Dispose();
        }
    }
}
