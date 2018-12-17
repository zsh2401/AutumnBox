using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    public sealed class CommandExecutor : INotifyOutput, IDisposable, IReceiveOutputByTo<CommandExecutor>
    {
        public class Result
        {
            public int ExitCode { get; }
            public Output Output { get; }
            public Result(Output output, int exitCode)
            {
                Output = output ?? throw new ArgumentNullException(nameof(output));
                ExitCode = exitCode;
            }
        }
        public event OutputReceivedEventHandler OutputReceived;

        public void KillCurrent()
        {
            currentProcess?.KillByTaskKill();
        }

        public async Task<Result> CmdAsync(params string[] args)
        {
            return await Task.Run(() =>
            {
                return Cmd(args);
            });
        }
        public async Task<Result> AdbShellAsync(IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return AdbShell(device, args);
            });
        }
        public async Task<Result> FastbootAsync(IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Fastboot(device, args);
            });
        }
        public async Task<Result> AdbAsync(IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Adb(device, args);
            });
        }
        public async Task<Result> FastbootAsync(params string[] args)
        {
            return await Task.Run(() =>
            {
                return Fastboot(args);
            });
        }
        public async Task<Result> AdbAsync(params string[] args)
        {
            return await Task.Run(() =>
             {
                 return Adb(args);
             });
        }

        public Result Cmd(params string[] args)
        {
            string joined = string.Join(" ", args);
            string _args = $"/c {joined}";
            return Execute("cmd.exe", _args);
        }
        public Result AdbShell(IDevice device, params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            return Adb(device, $"shell {joined}");
        }
        public Result Fastboot(IDevice device, params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-s {device.SerialNumber} {joined}";
            return Execute(exe.FullName, compCommand);
        }
        public Result Adb(IDevice device, params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.AdbFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-P{adbServer.Port} -s {device.SerialNumber} {joined}";
            return Execute(exe.FullName, compCommand);
        }
        public Result Fastboot(params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            string compCommand = $"{joined}";
            return Execute(exe.FullName, compCommand);
        }
        public Result Adb(params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.AdbFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-P{adbServer.Port} {joined}";
            return Execute(exe.FullName, compCommand);
        }

        public void Write(char _char)
        {
            currentProcess.StandardInput.Write(_char);
        }
        public void Write(string str)
        {
            currentProcess.StandardInput.Write(str);
        }
        public void WriteLine(string line)
        {
            currentProcess.StandardInput.WriteLine(line);
        }

        private Action<OutputReceivedEventArgs> callback;
        private Process currentProcess;
        private readonly OutputBuilder outputBuilder = new OutputBuilder();
        private readonly object _lock = new object();

        public Result Execute(string fileName, string args)
        {
            lock (_lock)
            {
                if (disposedValue)
                {
                    throw new ObjectDisposedException(nameof(CommandExecutor));
                }
                outputBuilder.Clear();
                var pStartInfo = new ProcessStartInfo()
                {
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = fileName,
                    Arguments = args,
                };
                currentProcess = Process.Start(pStartInfo);
                currentProcess.OutputDataReceived += (s, e) => RaiseOutputReceived(e, false);
                currentProcess.ErrorDataReceived += (s, e) => RaiseOutputReceived(e, true);
                currentProcess.BeginOutputReadLine();
                currentProcess.BeginErrorReadLine();
                currentProcess.WaitForExit();
                currentProcess.CancelErrorRead();
                currentProcess.CancelOutputRead();
                int exitCode = currentProcess.ExitCode;
                try { currentProcess?.Dispose(); currentProcess = null; } catch { }
                return new Result(outputBuilder.Result, exitCode);
            }
        }
        private void RaiseOutputReceived(DataReceivedEventArgs e, bool isErr)
        {
            callback?.Invoke(new OutputReceivedEventArgs(e, isErr));
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, isErr));
        }

        public CommandExecutor()
        {
            outputBuilder.Register(this);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                KillCurrent();
                currentProcess?.Dispose();
                currentProcess = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~CommandExecutor() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        public CommandExecutor To(Action<OutputReceivedEventArgs> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            return this;
        }
        #endregion
    }
}
