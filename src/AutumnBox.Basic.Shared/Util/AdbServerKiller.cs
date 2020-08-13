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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    public sealed class AdbServerKiller
    {
        private const int MAX_TIMEOUT = 3000;
        private readonly ICommandProcedure commandKillServer;

        public AdbServerKiller(ICommandProcedure killServerCommand)
        {
            this.commandKillServer = killServerCommand ?? throw new ArgumentNullException(nameof(killServerCommand));
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
                if (commandKillServer.Status == CommandStatus.Executing)
                {
                    commandKillServer.Cancel();
                    commandKillServer.Dispose();
                    KilProcesses();
                }
            });

        }
        private void KilProcesses()
        {
            using var cmd = new CommandProcedure()
            {
                FileName = "taskkill",
                Arguments = "/f /im adb.exe"
            };
            cmd.OutputReceived += (s, e) =>
            {
                SLogger<AdbServerKiller>.Info(e.Text);
            };
            cmd.Execute();
        }
    }
}
