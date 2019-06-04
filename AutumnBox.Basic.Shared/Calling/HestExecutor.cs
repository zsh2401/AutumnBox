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
    /// <summary>
    /// 优化的命令执行器
    /// </summary>
    public class HestExecutor : ICommandExecutor
    {
        private class HestExecutorResult : ICommandResult
        {
            public int ExitCode { get; set; } = 0;

            public Output Output { get; set; } = null;
        }
        /// <summary>
        /// 执行器被析构时触发
        /// </summary>
        public event EventHandler Disposed;
        /// <summary>
        /// 开始执行一条命令时触发
        /// </summary>
        public event CommandExecutingEventHandler CommandExecuting;
        /// <summary>
        /// 一条命令执行完毕时触发
        /// </summary>
        public event CommandExecutedEventHandler CommandExecuted;
        /// <summary>
        /// 接收到输出信息时触发
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 当前执行中的命令进程
        /// </summary>
        private Process currentProcess;
        /// <summary>
        /// 输出内容构造器
        /// </summary>
        private readonly OutputBuilder outputBuilder = new OutputBuilder();
        /// <summary>
        /// 获取启动信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 执行锁,确保随时只有一条命令在执行
        /// </summary>
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
        /// <summary>
        /// 是否已析构的依据
        /// </summary>
        private bool isDisposed = false;
        /// <summary>
        /// 析构本执行器
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
            CancelCurrent();
            try { Disposed?.Invoke(this, new EventArgs()); } catch { }
        }
        /// <summary>
        /// 取消当前执行的任务
        /// </summary>
        public void CancelCurrent()
        {
            if (currentProcess != null)
                this.KillProcessAndChildren(currentProcess.Id);
        }
        /// <summary>
        /// 寻找并杀死进程及子进程
        /// </summary>
        /// <param name="pid"></param>
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
        /// <summary>
        /// 结束一个进程(不用Process.Kill的原因是其不够高效)
        /// </summary>
        /// <param name="pid"></param>
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
