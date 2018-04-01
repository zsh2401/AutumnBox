/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 15:15:53
** filename: CmdExecuter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 万物基于命令执行器(滑稽)
    /// </summary>
    public sealed class CommandExecuter : IOutputable
    {
        /// <summary>
        /// 接收到输出时发生
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 进程开始时发生
        /// </summary>
        public event ProcessStartedEventHandler ProcessStarted;
        private ProcessStartInfo _startInfo;
        private OutputBuilder _builder;
        private readonly Object _lock;
        /// <summary>
        /// 静态的公用命令执行器
        /// </summary>
        public static readonly CommandExecuter Static;
        static CommandExecuter()
        {
            Static = new CommandExecuter();
        }
        /// <summary>
        /// 构建
        /// </summary>
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
        /// <summary>
        /// 执行一段命令或运行某个程序
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="args">命令或参数</param>
        /// <returns></returns>
        public AdvanceOutput Execute(string fileName, string args)
        {
            lock (_lock)
            {
                _builder.Clear();
                int exitCode = 404;
                _startInfo.FileName = fileName;
                _startInfo.Arguments = args;
                using (var process = Process.Start(_startInfo))
                {
                    process.OutputDataReceived += (s, e) => RaiseOutputReceived(e, false);
                    process.ErrorDataReceived += (s, e) => RaiseOutputReceived(e, true);
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    ProcessStarted?.Invoke(this, new ProcessStartedEventArgs(process.Id));
                    process.WaitForExit();
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    exitCode = process.ExitCode;
                };
                return new AdvanceOutput(_builder.ToOutput(), exitCode);
            }
        }
        /// <summary>
        /// 执行一个命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public AdvanceOutput Execute(Command cmd)
        {
            return Execute(cmd.FileName, cmd.Args);
        }
        /// <summary>
        /// 返回码的正则配对
        /// </summary>
        private const string exitCodePattern = @"exitcode(?<code>\d+)";
        
        /// <summary>
        /// 便捷的执行一段shell代码,但更建议使用AndroidShellV2来完成
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public AdvanceOutput QuicklyShell(DeviceSerialNumber dev, string command)
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
        /// <summary>
        /// 处理并触发OutputReceived事件时发生
        /// </summary>
        /// <param name="src"></param>
        /// <param name="isError"></param>
        /// <param name="e"></param>
        private void RaiseOutputReceived(DataReceivedEventArgs src ,bool isError = false)
        {
            if (src.Data == null) return;
            if (src.Data == "") return;
            var match = Regex.Match(src.Data, exitCodePattern);
            if (match.Success)
            {
                leastShellExitCode = int.Parse(match.Result("${code}"));
                return;
            }
            if (!isError)
            {
                _builder.AppendOut(src.Data);
            }
            else
            {
                _builder.AppendError(src.Data);
            }
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(src,isError));
        }
    }
}
