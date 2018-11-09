using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util.Debugging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Basic.Calling.Adb
{
   public class RealtimeShellCommand : AdbCommand
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
            proc.StandardInput.WriteLine("exit");
        }
        protected override void RaiseOutputReceived(DataReceivedEventArgs e, bool isErr)
        {
            base.RaiseOutputReceived(e, isErr);
            Trace.WriteLine(e.Data);
            //new Logger<RealtimeShellCommand>().Info(e.Data);
        }
    }
}
