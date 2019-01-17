using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 命令执行器
    /// </summary>
    public class CommandExecutor : INotifyOutput, IDisposable, IReceiveOutputByTo<CommandExecutor>
    {
        /// <summary>
        /// 进程事件
        /// </summary>
        public class ProcessEventArgs : EventArgs
        {
            /// <summary>
            /// 进程
            /// </summary>
            public Process Process { get; set; }
            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="process"></param>
            public ProcessEventArgs(Process process) { }
        }
        /// <summary>
        /// 进程开始了
        /// </summary>
        public event EventHandler<ProcessEventArgs> ProcessStarted;
        /// <summary>
        /// 进程结束了
        /// </summary>
        public event EventHandler<ProcessEventArgs> ProcessExited;
        /// <summary>
        /// 命令执行器的结果
        /// </summary>
        public class Result
        {
            /// <summary>
            /// 返回码
            /// </summary>
            public virtual int ExitCode { get; }
            /// <summary>
            /// 输出内容
            /// </summary>
            public virtual Output Output { get; }
            /// <summary>
            /// 结果
            /// </summary>
            /// <param name="output"></param>
            /// <param name="exitCode"></param>
            public Result(Output output, int exitCode)
            {
                Output = output ?? throw new ArgumentNullException(nameof(output));
                ExitCode = exitCode;
            }
            /// <summary>
            /// 结果
            /// </summary>
            protected Result() { }
        }
        /// <summary>
        /// 当接收到输出时发生
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 杀死当前正在执行的进程
        /// </summary>
        public virtual void KillCurrent()
        {
            currentProcess?.KillByTaskKill();
        }
        /// <summary>
        /// 异步执行CMD命令
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<Result> CmdAsync(params string[] args)
        {
            return await Task.Run(() =>
            {
                return Cmd(args);
            });
        }
        /// <summary>
        /// 异步执行ADB SHELL命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<Result> AdbShellAsync(IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return AdbShell(device, args);
            });
        }
        /// <summary>
        /// 异步执行针对Fastboot命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<Result> FastbootAsync(IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Fastboot(device, args);
            });
        }
        /// <summary>
        /// 异步执行针对设备的ADB命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<Result> AdbAsync(IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Adb(device, args);
            });
        }
        /// <summary>
        /// 异步执行Fastboot命令
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<Result> FastbootAsync(params string[] args)
        {
            return await Task.Run(() =>
            {
                return Fastboot(args);
            });
        }
        /// <summary>
        /// 异步执行ADB命令
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<Result> AdbAsync(params string[] args)
        {
            return await Task.Run(() =>
             {
                 return Adb(args);
             });
        }
        /// <summary>
        /// CMD
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result Cmd(params string[] args)
        {
            string joined = string.Join(" ", args);
            string _args = $"/c {joined}";
            return Execute("cmd.exe", _args);
        }
        /// <summary>
        /// 执行adb shell命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result AdbShell(IDevice device, params string[] args)
        {
            if (!ManagedAdb.Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("adb server is killed");
            }
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            return Adb(device, $"shell {joined}");
        }
        /// <summary>
        /// 执行fastboot命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result Fastboot(IDevice device, params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-s {device.SerialNumber} {joined}";
            return Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 执行针对设备的adb命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result Adb(IDevice device, params string[] args)
        {
            if (!ManagedAdb.Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("adb server is killed");
            }
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.AdbFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-P{adbServer.Port} -s {device.SerialNumber} {joined}";
            return Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 执行非针对设备的fastboot命令
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result Fastboot(params string[] args)
        {
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            string compCommand = $"{joined}";
            return Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 执行非针对设备的adb命令
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result Adb(params string[] args)
        {
            if (!ManagedAdb.Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("adb server is killed");
            }
            IAdbServer adbServer = AutumnBox.Basic.ManagedAdb.Adb.Server;
            FileInfo exe = AutumnBox.Basic.ManagedAdb.Adb.AdbFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-P{adbServer.Port} {joined}";
            return Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 向标准输入流输入一个字符
        /// </summary>
        /// <param name="_char"></param>
        public virtual void Write(char _char)
        {
            currentProcess.StandardInput.Write(_char);
        }
        /// <summary>
        /// 向标准输入流输入一串字符
        /// </summary>
        /// <param name="str"></param>
        public virtual void Write(string str)
        {
            currentProcess.StandardInput.Write(str);
        }
        /// <summary>
        /// 想标准输入流输入一行字符
        /// </summary>
        /// <param name="line"></param>
        public virtual void WriteLine(string line)
        {
            currentProcess.StandardInput.WriteLine(line);
        }

        /// <summary>
        /// TO订阅的callback
        /// </summary>
        protected Action<OutputReceivedEventArgs> callback;
        /// <summary>
        /// 当前进程
        /// </summary>
        protected Process currentProcess;
        /// <summary>
        /// 输出构造器
        /// </summary>
        protected readonly OutputBuilder outputBuilder = new OutputBuilder();
        /// <summary>
        /// 执行锁
        /// </summary>
        protected readonly object _lock = new object();

        /// <summary>
        /// 综合进行判断,是否开启新窗口
        /// </summary>
        protected bool CreateNoWindow
        {
            get
            {
                return this.NeverCreateNewWindow || !Settings.CreateNewWindow;
            }
        }
        /// <summary>
        /// 获取进程启动信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual ProcessStartInfo GetStartInfo(string fileName, string args)
        {
            return new ProcessStartInfo()
            {
                RedirectStandardError = CreateNoWindow,
                RedirectStandardOutput = CreateNoWindow,
                RedirectStandardInput = CreateNoWindow,
                UseShellExecute = false,
                CreateNoWindow = CreateNoWindow,
                FileName = fileName,
                Arguments = args,
            };
        }

        /// <summary>
        /// 进程启动了,进行一些流处理等操作
        /// </summary>
        /// <param name="process"></param>
        protected virtual void OnProcessStarted(Process process)
        {

            if (process.StartInfo.RedirectStandardOutput)
            {
                process.OutputDataReceived += (s, e) => RaiseOutputReceived(e, false);
                process.ErrorDataReceived += (s, e) => RaiseOutputReceived(e, true);
                currentProcess.BeginOutputReadLine();
                currentProcess.BeginErrorReadLine();
            }
            ProcessStarted?.Invoke(this, new ProcessEventArgs(process));
        }

        /// <summary>
        /// 进程结束了,释放资源
        /// </summary>
        /// <param name="process"></param>
        protected virtual void OnProcessExited(Process process)
        {
            ProcessExited?.Invoke(this, new ProcessEventArgs(process));
            if (process.StartInfo.RedirectStandardOutput)
            {
                process.CancelErrorRead();
                process.CancelOutputRead();
            }
            try { currentProcess?.Dispose(); } catch { }
        }

        /// <summary>
        /// 无锁的执行..不建议覆写
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual Result ExecuteWithoutLock(string fileName, string args)
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(CommandExecutor));
            }
            int exitCode;
            ProcessStartInfo pStartInfo;
            outputBuilder.Clear();
            pStartInfo = GetStartInfo(fileName, args);
            currentProcess = Process.Start(pStartInfo);
            OnProcessStarted(currentProcess);
            currentProcess.WaitForExit();
            exitCode = currentProcess.ExitCode;
            OnProcessExited(currentProcess);
            currentProcess = null;
            return new Result(outputBuilder.Result, exitCode);
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Result Execute(string fileName, string args)
        {
            lock (_lock)
            {
                return ExecuteWithoutLock(fileName, args);
            }
        }

        /// <summary>
        /// 触发输出事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isErr"></param>
        protected virtual void RaiseOutputReceived(DataReceivedEventArgs e, bool isErr)
        {
            callback?.Invoke(new OutputReceivedEventArgs(e, isErr));
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, isErr));
        }

        /// <summary>
        /// 设置此值为true,则无论如何都不可能显示CMD窗口
        /// </summary>
        public bool NeverCreateNewWindow { get; set; } = false;

        /// <summary>
        /// 构造CommandExecutor
        /// </summary>
        public CommandExecutor()
        {
            outputBuilder.Register(this);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// protected Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
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
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 通过TO模式进行输出的订阅
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual CommandExecutor To(Action<OutputReceivedEventArgs> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            return this;
        }
        #endregion
    }
}
