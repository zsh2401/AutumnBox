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
    public class MyCommandProcedure : ICommandProcedure, INotifyDisposed
    {
        private Process? process;
        private OutputBuilder? outputBuilder;

        public CommandStatus Status { get; private set; } = CommandStatus.Ready;

        public ICommandResult Result
        {
            get
            {
                if (Status == CommandStatus.Ready || Status == CommandStatus.Executing)
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
        private ICommandResult _result = new MyCommandResult();

        public Exception? Exception { get; set; }

        public event OutputReceivedEventHandler? OutputReceived;
        public event EventHandler? Finished;
        public event EventHandler Disposed;

        public bool KillChildWhenDisposing { get; set; } = true;
        public MyCommandProcedure(string fileName,
            ushort port,
            DirectoryInfo adbToolsDir,
            params string[] args
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
            string pathEnv = System.Environment.GetEnvironmentVariable("PATH");
            var newPath = $"{adbToolsDir.FullName};{pathEnv}";
            process.StartInfo.EnvironmentVariables["PATH"] = newPath;
            process.StartInfo.EnvironmentVariables["ANDROID_ADB_SERVER_PORT"] = port.ToString();
        }

        public ICommandResult Execute()
        {
            if (Status != CommandStatus.Ready)
            {
                throw new InvalidOperationException("Command procedure is not ready!");
            }
            try
            {
                Status = CommandStatus.Executing;
                process!.Start();
                process.OutputDataReceived += Process_OutputDataReceived;
                process.ErrorDataReceived += Process_ErrorDataReceived;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.CancelErrorRead();
                process.CancelOutputRead();
                this.Status = CommandStatus.Succeeded;
                Result = new MyCommandResult()
                {
                    ExitCode = process.ExitCode,
                    Output = outputBuilder!.ToOutput()
                };
            }
            catch (Exception e)
            {
                Exception = e;
                Status = CommandStatus.Failed;
                Result = new MyCommandResult()
                {
                    ExitCode = -2401,
                    Output = outputBuilder!.ToOutput()
                };
            }
            Finished?.Invoke(this, new EventArgs());
            return Result;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputBuilder!.AppendOut(e.Data);
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, true));
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputBuilder!.AppendOut(e.Data);
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, false));
        }

        private class MyCommandResult : ICommandResult
        {
            public int? ExitCode { get; set; } = null;

            public Output Output { get; set; } = new Output();
        }

        public Task<ICommandResult> ExecuteAsync()
        {
            if (Status != CommandStatus.Ready)
            {
                throw new InvalidOperationException("Command procedure is not ready!");
            }
            return Task.Run(Execute);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用


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


        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MyCommandProcedure()
        // {
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
        #endregion

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
