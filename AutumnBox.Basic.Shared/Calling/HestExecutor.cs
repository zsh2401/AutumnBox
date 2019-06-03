using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Data;
using AutumnBox.Logging;

namespace AutumnBox.Basic.Calling
{
    public class HestExecutor : ICommandExecutor
    {
        private class HestExecutorResult : ICommandResult
        {
            public int ExitCode { get; set; } = 0;

            public Output Output { get; set; } = null;
        }
        public event EventHandler Disposed;
        public event EventHandler<CommandExecutingEventArgs> CommandExecuting;
        public event EventHandler<CommandExecutedEventArgs> CommandExecuted;
        public event EventHandler<OutputReceivedEventArgs> OutputReceived;
        private Process currentProcess;
        private readonly OutputBuilder outputBuilder = new OutputBuilder();
        private ProcessStartInfo GetStartInfo(string fileName, string args)
        {
            return new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
        }
        private readonly object _executingLock = new object();

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ICommandResult Execute(string fileName, string args)
        {
            lock (_executingLock)
            {
                if (isDisposed) throw new ObjectDisposedException(nameof(HestExecutor));
                //输出构造器
                outputBuilder.Clear();
                //记录开始时间
                DateTime start = DateTime.Now;
                //开始进程
                currentProcess = Process.Start(GetStartInfo(fileName, args));

                //监听
                currentProcess.OutputDataReceived += (s, e) =>
                {
                    outputBuilder.AppendOut(e.Data);
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, false));
                };
                currentProcess.ErrorDataReceived += (s, e) =>
                {
                    outputBuilder.AppendError(e.Data);
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, true));
                };
                currentProcess.BeginOutputReadLine();
                currentProcess.BeginErrorReadLine();

                //触发事件
                CommandExecuting?.Invoke(this, new CommandExecutingEventArgs(fileName, args));

                //等待进程结束
                currentProcess.WaitForExit();
                currentProcess.CancelErrorRead();
                currentProcess.CancelOutputRead();

                //记录结束时间
                DateTime end = DateTime.Now;
                //构造结果对象
                var result = new HestExecutorResult() { ExitCode = currentProcess.ExitCode, Output = outputBuilder.Result };
                //触发结束事件
                CommandExecuted?.Invoke(this, new CommandExecutedEventArgs(fileName, args, result, end - start));
                //返回结果
                return result;
            };
        }

        private bool isDisposed = false;

        /// <summary>
        /// 析构本执行器
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
            CancelCurrent();
        }
        /// <summary>
        /// 取消当前执行的任务
        /// </summary>
        public void CancelCurrent()
        {
            if (currentProcess != null)
                this.KillProcessAndChildren(currentProcess.Id);
        }

        private void KillProcessAndChildren(int pid)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
                ManagementObjectCollection moc = searcher.Get();
                foreach (ManagementObject mo in moc)
                {
                    KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }
                Kill(pid);
            }
            catch (Exception e)
            {
                SLogger<HestExecutor>.Warn("Can not cancel current command", e);
                /* process already exited */
            }
        }
        private void Kill(int pid)
        {
            new WindowsCmdCommand($"taskkill /F /PID {pid}")
                .To((e) =>
                {
                    SLogger<HestExecutor>.Debug(e.Text);
                })
                .Execute();
        }
    }
}
