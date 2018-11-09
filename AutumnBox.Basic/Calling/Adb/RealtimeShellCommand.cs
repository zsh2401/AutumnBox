using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Basic.Calling.Adb
{
    class RealtimeShellCommand : AdbCommand
    {
        private readonly string shellCommand;
        public RealtimeShellCommand(IDevice device,string shellCommand) : base(device,"shell")
        {
            this.shellCommand = shellCommand ?? throw new ArgumentNullException(nameof(shellCommand));
        }
        protected override void BeforeProcessStart(ProcessStartInfo procStartInfo)
        {
            base.BeforeProcessStart(procStartInfo);
            procStartInfo.RedirectStandardInput = true;
        }
        protected override void OnProcessStarted(Process proc)
        {
            base.OnProcessStarted(proc);
            proc.StandardInput.WriteLine(shellCommand);
            proc.StandardInput.WriteLine("echo $?");
            proc.StandardInput.WriteLine("exit");
        }
    }
}
