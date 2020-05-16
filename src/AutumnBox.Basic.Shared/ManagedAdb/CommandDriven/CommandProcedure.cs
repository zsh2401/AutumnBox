#nullable enable
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 基础的命令流程
    /// </summary>
    public sealed class CommandProcedure : ICommandProcedure, INotifyDisposed
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
        public Exception? Exception { get; private set; }

        /// <inheritdoc/>
        public event OutputReceivedEventHandler? OutputReceived;

        /// <inheritdoc/>
        public event EventHandler? Finished;

        /// <inheritdoc/>
        public event EventHandler? Executing;

        /// <summary>
        /// 指示析构模式
        /// </summary>
        public bool KillChildProcessWhenDisposing { get; set; } = true;

        /// <summary>
        /// 目标文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 参数
        /// </summary>
        public string Arguments { get; set; } = string.Empty;

        /// <summary>
        /// 直接执行亦或使用cmd.exe套壳执行
        /// </summary>
        public bool DirectExecute { get; set; } = false;

        /// <summary>
        /// 额外的Path环境变量
        /// </summary>
        public List<string> ExtraPathVariables { get; } = new List<string>();

        /// <summary>
        /// 额外的环境变量
        /// </summary>
        public StringDictionary ExtraEnvironmentVariables { get; } = new StringDictionary();

        /// <summary>
        /// 构建命令
        /// </summary>
        public CommandProcedure()
        {
            outputBuilder = new OutputBuilder();
        }

        /// <inheritdoc/>
        public event EventHandler? Disposed;

        /// <inheritdoc/>
        public CommandResult Execute()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(CommandProcedure));
            }
            if (Status != CommandStatus.Ready)
            {
                throw new InvalidOperationException("Command procedure is not ready!");
            }
            try
            {
                Status = CommandStatus.Executing;
                Executing?.Invoke(this, new EventArgs());
                process = Process.Start(GetStartInfo());
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
        /// 获取进程启动信息
        /// </summary>
        /// <returns></returns>
        private ProcessStartInfo GetStartInfo()
        {
            var fileName = DirectExecute ? FileName : "cmd.exe";
            var args = DirectExecute ? Arguments : $"/c {FileName} {Arguments}";
            var pStartInfo = new ProcessStartInfo(fileName, args)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            StringBuilder pathEnvSb = new StringBuilder(pStartInfo.EnvironmentVariables[CommandDrivenHelper.ENV_KEY_PATH]);
            ExtraPathVariables.ForEach(p =>
            {
                pathEnvSb.Insert(0, $"{p};");
            });
            pStartInfo.EnvironmentVariables[CommandDrivenHelper.ENV_KEY_PATH] = pathEnvSb.ToString();

            foreach (string key in ExtraEnvironmentVariables.Keys)
            {
                pStartInfo.EnvironmentVariables[key] = ExtraEnvironmentVariables[key];
            }
            return pStartInfo;
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
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(CommandProcedure));
            }
            return Task.Run(() =>
            {
                System.Threading.Thread.CurrentThread.Name = $"Command Procedure Thread: {FileName} {string.Join(" ", Arguments)}";
                return Execute();
            });
        }

        /// <inheritdoc/>
        public void Cancel()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(CommandProcedure));
            }
            if (Status == CommandStatus.Executing)
            {
                if (KillChildProcessWhenDisposing)
                {
                    GreateKill(process!.Id);
                }
                else
                {
                    process!.Kill();
                }
            }
        }

        /// <summary>
        /// "强杀进程"
        /// </summary>
        /// <param name="pid"></param>
        private static void GreateKill(int pid)
        {
            ProcessKiller.FKill(pid);
        }

        #region IDisposable Support

        /// <summary>
        /// 指示是否被释放
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// 内部的虚析构函数
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (Status != CommandStatus.Ready)
                    {
                        if (process != null)
                        {
                            process.OutputDataReceived -= Process_OutputDataReceived;
                            process.ErrorDataReceived -= Process_ErrorDataReceived;
                            process.Dispose();
                        }
                    }
                }
                if (Status == CommandStatus.Executing)
                {
                    Cancel();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                process = null;
                outputBuilder = null;
                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 终结器
        /// </summary>
        ~CommandProcedure()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}