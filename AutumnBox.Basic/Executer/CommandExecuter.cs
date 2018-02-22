/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 15:15:53
** filename: CmdExecuter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Executer
{
    public sealed class CommandExecuter : IOutputable
    {
        public event OutputReceivedEventHandler OutputReceived;
        public event ProcessStartedEventHandler ProcessStarted;
        private ProcessStartInfo _startInfo;
        private OutputBuilder _builder;
        private readonly Object _lock;
        public static readonly CommandExecuter Static;
        static CommandExecuter()
        {
            Static = new CommandExecuter();
        }
        public CommandExecuter()
        {
            _builder = new OutputBuilder();
            _lock = new object();
            _startInfo = new ProcessStartInfo()
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
        }
        public AdvanceOutput Execute(string fileName, string command)
        {
            lock (_lock)
            {
                _builder.Clear();
                int exitCode = 404;
                _startInfo.FileName = fileName;
                _startInfo.Arguments = command;
                using (var process = Process.Start(_startInfo))
                {
                    process.OutputDataReceived += (s, e) => OnOutputReceived(new OutputReceivedEventArgs(e, false));
                    process.ErrorDataReceived += (s, e) => OnOutputReceived(new OutputReceivedEventArgs(e, true));
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = process.Id });
                    process.WaitForExit();
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    exitCode = process.ExitCode;
                };
                return new AdvanceOutput(_builder.ToOutput(), exitCode);
            }
        }
        public AdvanceOutput Execute(Command cmd)
        {
            return Execute(cmd.FileName, cmd.ToString());
        }
        private const string exitCodePattern = @"exitcode(?<code>\d+)";
        public AdvanceOutput QuicklyShell(DeviceSerial dev, string command)
        {
            leastShellExitCode = null;
            var cmd = Command.MakeForAdb(dev, $"shell \"{command} ; echo exitcode$?\"");
            var result = Execute(cmd);
            var match = Regex.Match(result.ToString(), exitCodePattern);
            int exitCode = 1;
            if (match.Success)
            {
                exitCode = int.Parse(match.Result("${code}"));
            }
            return new AdvanceOutput(result, leastShellExitCode ?? 1);
        }
        private int? leastShellExitCode = 1;
        private void OnOutputReceived(OutputReceivedEventArgs e)
        {
            if (e.Text == null) return;
            if (e.Text == "") return;
            var match = Regex.Match(e.Text, exitCodePattern);
            if (match.Success)
            {
                leastShellExitCode = int.Parse(match.Result("${code}"));
                return;
            }
            if (!e.IsError)
            {
                _builder.AppendOut(e.Text);
            }
            else
            {
                _builder.AppendError(e.Text);
            }
            OutputReceived?.Invoke(this, e);
        }
    }
}
