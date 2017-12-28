/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 15:15:53
** filename: CmdExecuter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Connection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class CommandExecuter : IOutSender
    {
        public event OutputReceivedEventHandler OutputReceived;
        public event ProcessStartedEventHandler ProcessStarted;
        private ProcessStartInfo _startInfo;
        private OutputData _buffer;
        private readonly Object _lock;
        public CommandExecuter()
        {
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
        public CommandExecuterResult Execute(string fileName, string command)
        {
            lock (_lock)
            {
                _buffer = new OutputData();
                int exitCode = 404;
                _startInfo.FileName = fileName;
                _startInfo.Arguments = command;
                using (var process = Process.Start(_startInfo))
                {
                    process.OutputDataReceived += (s, e) => OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false));
                    process.ErrorDataReceived += (s, e) => OnOutputReceived(new OutputReceivedEventArgs(e.Data, e, false));
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = process.Id });
                    process.WaitForExit();
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    exitCode = process.ExitCode;
                };
                return new CommandExecuterResult(_buffer, exitCode);
            }
        }
        public CommandExecuterResult Execute(Command cmd)
        {
            return Execute(cmd.FileName, cmd.FullCommand);
        }
        private const string exitCodePattern = @"exitcode(?<code>\d+)";
        public CommandExecuterResult QuicklyShell(Serial dev, string command)
        {
            var result = Execute(Command.MakeForAdb(dev, $"shell \"{command};echo exitcode$?\""));
            var match = Regex.Match(result.Output.ToString(), exitCodePattern);
            int exitCode = 1;
            if (match.Success)
            {
                exitCode = int.Parse(match.Result("${code}"));
            }
            return new CommandExecuterResult(result.Output, exitCode);
        }
        private void OnOutputReceived(OutputReceivedEventArgs e)
        {
            if (e.Text == null) return;
            if (e.Text == "") return;
            if (!e.IsError)
            {
                _buffer.OutAdd(e.Text);
            }
            else
            {
                _buffer.ErrorAdd(e.Text);
            }
            OutputReceived?.Invoke(this, e);
        }
    }
}
