/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 2:45:43 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 基于进程的命令
    /// </summary>
    public class ProcessBasedCommand : IProcessBasedCommand
    {
        /// <summary>
        /// 不创建新窗口
        /// </summary>
        public bool NeverCreateNewWindow { get; set; } = false;
        private class Result : IProcessBasedCommandResult
        {
            public Output Output { get; set; }

            public int ExitCode { get; set; }
        }
        /// <summary>
        /// 输出内容构造器
        /// </summary>
        private OutputBuilder outputBuilder = new OutputBuilder();
        /// <summary>
        /// 进程
        /// </summary>
        internal Process process;
        /// <summary>
        /// 进程开始信息
        /// </summary>
        private ProcessStartInfo processStartInfo;
        /// <summary>
        /// 输出事件
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        private Action<OutputReceivedEventArgs> callback;
        /// <summary>
        /// 添加Callback
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IProcessBasedCommand To(Action<OutputReceivedEventArgs> callback)
        {
            this.callback = callback;
            return this;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="executableFile"></param>
        /// <param name="args"></param>
        public ProcessBasedCommand(string executableFile, string args)
        {
            if (string.IsNullOrWhiteSpace(executableFile))
            {
                throw new ArgumentException("message", nameof(executableFile));
            }

            outputBuilder.Register(this);
            this.executableFile = executableFile;
            this.args = args;
        }
        /// <summary>
        /// 无参数的构造
        /// </summary>
        /// <param name="executableFile"></param>
        public ProcessBasedCommand(string executableFile) : this(executableFile, null) { }
        private readonly object executeLock = new object();
        private readonly string executableFile;
        private readonly string args;

        /// <summary>
        /// 在进程开始前调用
        /// </summary>
        /// <param name="procStartInfo"></param>
        protected virtual void BeforeProcessStart(ProcessStartInfo procStartInfo)
        {

        }
        /// <summary>
        /// 当进程启动后调用
        /// </summary>
        /// <param name="proc"></param>
        protected virtual void OnProcessStarted(Process proc)
        {

        }
        /// <summary>
        /// 注册进程事件
        /// </summary>
        /// <param name="proc"></param>
        protected virtual void RegisterProcessEvent(Process proc)
        {
            if (proc.StartInfo.CreateNoWindow)
            {
                proc.OutputDataReceived += (s, e) => RaiseOutputReceived(e, false);
                proc.ErrorDataReceived += (s, e) => RaiseOutputReceived(e, true);
            }
        }
        /// <summary>
        /// 在准备等待进程前调用
        /// </summary>
        /// <param name="proc"></param>
        protected virtual void BeforeWaiting(Process proc)
        {

        }
        /// <summary>
        /// 当进程结束时调用
        /// </summary>
        /// <param name="proc"></param>
        protected virtual void OnProcessExited(Process proc)
        {
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public virtual IProcessBasedCommandResult Execute()
        {
            lock (executeLock)
            {
                processStartInfo = new ProcessStartInfo()
                {
                    RedirectStandardError = NeverCreateNewWindow || !Settings.CreateNewWindow,
                    RedirectStandardOutput = NeverCreateNewWindow || !Settings.CreateNewWindow,
                    RedirectStandardInput = false,
                    UseShellExecute = false,
                    CreateNoWindow = NeverCreateNewWindow || !Settings.CreateNewWindow,
                    FileName = executableFile,
                    Arguments = args,
                };
                if (ManagedAdb.Adb.Manager?.Server?.IsEnable == true)
                {
                    string pathEnv = processStartInfo.EnvironmentVariables["path"];
                    processStartInfo.EnvironmentVariables["path"] = $"{ManagedAdb.Adb.AdbToolsDir.FullName};{pathEnv}";
                    processStartInfo.EnvironmentVariables["ANDROID_ADB_SERVER_PORT"] = ManagedAdb.Adb.Server.Port.ToString();
                }
                outputBuilder.Clear();
                int retCode = -1;
                BeforeProcessStart(processStartInfo);
                using (process = Process.Start(processStartInfo))
                {
                    OnProcessStarted(process);
                    RegisterProcessEvent(process);
                    if (process.StartInfo.CreateNoWindow)
                    {
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                    }

                    BeforeWaiting(process);
                    process.WaitForExit();
                    if (process.StartInfo.CreateNoWindow)
                    {
                        process.CancelErrorRead();
                        process.CancelOutputRead();
                    }
                    retCode = process.ExitCode;
                    OnProcessExited(process);
                    process = null;
                }
                return new Result()
                {
                    Output = outputBuilder.Result,
                    ExitCode = retCode
                };
            }
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isErr"></param>
        protected virtual void RaiseOutputReceived(DataReceivedEventArgs e, bool isErr)
        {
            callback?.Invoke(new OutputReceivedEventArgs(e, isErr));
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, isErr));
        }
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <returns></returns>
        public virtual Task<IProcessBasedCommandResult> ExecuteAsync()
        {
            return Task.Run(() =>
            {
                return Execute();
            });
        }
        /// <summary>
        /// Command:Become string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return processStartInfo.FileName + " " + processStartInfo.Arguments;
        }
        /// <summary>
        /// 停止正在执行的命令
        /// </summary>
        public void Kill()
        {
            //process.Close();
            //process.Kill();
            process.KillByTaskKill();
        }
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
                                            /// <summary>
                                            /// 析构
                                            /// </summary>
                                            /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    outputBuilder.Clear();
                    outputBuilder.Unregister(this);
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                try
                {
                    process?.Kill();
                }
                catch { }
                outputBuilder = null;
                process = null;
                disposedValue = true;
            }
        }
        /// <summary>
        /// 析构
        /// </summary>
        ~ProcessBasedCommand()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }
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
        #endregion

    }
}
