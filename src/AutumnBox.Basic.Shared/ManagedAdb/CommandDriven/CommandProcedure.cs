#nullable enable
using AutumnBox.Basic.Data;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 基础的命令流程
    /// </summary>
    public class CommandProcedure : ICommandProcedure, INotifyDisposed
    {
        /// <summary>
        /// 当前正在执行的进程
        /// </summary>
        private Process? process;

        /// <summary>
        /// 输出内容构造器
        /// </summary>
        private OutputBuilder? outputBuilder;

        /// <inheritdoc/>
        public CommandStatus Status { get; private set; } = CommandStatus.Ready;

        /// <inheritdoc/>
        public CommandResult Result
        {
            get
            {
                if (_result == null || Status == CommandStatus.Ready || Status == CommandStatus.Executing)
                {
                    throw new InvalidOperationException("Command procedure is not finished");
                }
                return _result;
            }
            set
            {
                _result = value;
            }
        }
        private CommandResult? _result;

        /// <inheritdoc/>
        public Exception? Exception { get; set; }

        /// <inheritdoc/>
        public event OutputReceivedEventHandler? OutputReceived;

        /// <inheritdoc/>
        public event EventHandler? Finished;

        /// <inheritdoc/>
        public event EventHandler? Executing;

        /// <summary>
        /// 指示析构模式
        /// </summary>
        public bool KillChildWhenDisposing { get; set; } = true;

        /// <summary>
        /// 构建命令
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="port"></param>
        /// <param name="adbToolsDir"></param>
        /// <param name="args"></param>
        public CommandProcedure(string fileName, params string[] args
            )
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }


            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            outputBuilder = new OutputBuilder();
            process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = fileName,
                    Arguments = string.Join(" ", args),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                }
            };
        }

        /// <inheritdoc/>
        public event EventHandler? Disposed;

        /// <inheritdoc/>
        public CommandResult Execute()
        {
            if (Status != CommandStatus.Ready)
            {
                throw new InvalidOperationException("Command procedure is not ready!");
            }
            try
            {
                Status = CommandStatus.Executing;
                Executing?.Invoke(this, new EventArgs());
                process!.Start();
                process.OutputDataReceived += Process_OutputDataReceived;
                process.ErrorDataReceived += Process_ErrorDataReceived;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.CancelErrorRead();
                process.CancelOutputRead();
                this.Status = CommandStatus.Succeeded;
                Result = new CommandResult(process.ExitCode, outputBuilder!.ToOutput());
            }
            catch (Exception e)
            {
                Exception = e;
                Status = CommandStatus.Failed;
                Result = new CommandResult(1, outputBuilder!.ToOutput());
            }
            Finished?.Invoke(this, new EventArgs());
            return Result;
        }

        /// <summary>
        /// 处理进程的错误数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputBuilder!.AppendOut(e.Data);
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, true));
        }

        /// <summary>
        /// 处理进程的标准输出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputBuilder!.AppendOut(e.Data);
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, false));
        }

        /// <inheritdoc/>
        public Task<CommandResult> ExecuteAsync()
        {
            if (Status != CommandStatus.Ready)
            {
                throw new InvalidOperationException("Command procedure is not ready!");
            }
            return Task.Run(Execute);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 内部的虚析构函数
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
                process!.OutputDataReceived -= Process_OutputDataReceived;
                process!.ErrorDataReceived -= Process_ErrorDataReceived;
                Cancel();
                process.Dispose();

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                process = null;
                outputBuilder = null;
                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// "强杀进程"
        /// </summary>
        /// <param name="pid"></param>
        private static void GreateKill(int pid)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
                ManagementObjectCollection moc = searcher.Get();
                foreach (ManagementObject mo in moc)
                {
                    GreateKill(Convert.ToInt32(mo["ProcessID"]));
                }
                Process.GetProcessById(pid).Kill();
            }
            catch
            {
                //SLogger<MyCommandProcedure>.Warn("Can not kill process", e);
                /* process already exited */
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

        /// <inheritdoc/>
        public void Cancel()
        {
            if (Status == CommandStatus.Executing)
            {
                if (KillChildWhenDisposing)
                {
                    GreateKill(process!.Id);
                }
                else
                {
                    process!.Kill();
                }
            }
        }
    }
}
