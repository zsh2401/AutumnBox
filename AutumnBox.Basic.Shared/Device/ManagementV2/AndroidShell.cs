using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.Util;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.ManagementV2
{
    /// <summary>
    /// 提供一个长期存在的Shell控制功能
    /// </summary>
    public class AndroidShell : IDisposable, INotifyOutput, IReceiveOutputByTo<AndroidShell>
    {
        private IDevice device;
        private Process coreProcess;
        /// <summary>
        /// 设置此值为true,则无论如何都不可能显示CMD窗口
        /// </summary>
        public bool NeverCreateNewWindow { get; set; } = false;
        private bool CreateNoWindow
        {
            get
            {
                return this.NeverCreateNewWindow || !Settings.CreateNewWindow;
            }
        }
        /// <summary>
        /// 构造一个提供长久Shell的对象
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        public AndroidShell(IDevice device)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Open()
        {
            if (!Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("Server is not enable!");
            }
            var pInfo = new ProcessStartInfo()
            {
                RedirectStandardError = CreateNoWindow,
                RedirectStandardOutput = CreateNoWindow,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = CreateNoWindow,
                FileName = Adb.AdbFilePath,
                Arguments = $"-P{Adb.Server.Port} -s {device.SerialNumber} shell",
            };
            coreProcess = Process.Start(pInfo);
            coreProcess.OutputDataReceived += (s, e) => RaiseOutputReceived(e, false);
            coreProcess.ErrorDataReceived += (s, e) => RaiseOutputReceived(e, true);
            coreProcess.BeginOutputReadLine();
            coreProcess.BeginErrorReadLine();
        }

        /// <summary>
        /// TO订阅的callback
        /// </summary>
        protected Action<OutputReceivedEventArgs> callback;

        /// <summary>
        /// 触发输出事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isErr"></param>
        private void RaiseOutputReceived(DataReceivedEventArgs e, bool isErr)
        {
            callback?.Invoke(new OutputReceivedEventArgs(e, isErr));
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e, isErr));
        }

        /// <summary>
        /// 通过TO模式进行输出的订阅
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual AndroidShell To(Action<OutputReceivedEventArgs> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            return this;
        }

        /// <summary>
        /// 写一个字符
        /// </summary>
        /// <param name="_char"></param>
        public void Write(char _char)
        {
            coreProcess.StandardInput.Write(_char);
        }
        /// <summary>
        /// 写一行字符
        /// </summary>
        /// <param name="str"></param>
        public void WriteLine(string str)
        {
            coreProcess.StandardInput.WriteLine(str);
        }

        /// <summary>
        /// 等待到结束
        /// </summary>
        public void WaitForExit()
        {
            coreProcess.WaitForExit();
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public int ExitCode => coreProcess.ExitCode;
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 订阅输出事件
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;

        /// <summary>
        /// Dispose
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
                if (!coreProcess.HasExited)
                {
                    coreProcess.Kill();
                }

                coreProcess.Dispose();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                coreProcess = null;
                device = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AndroidShell() {
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
        #endregion

    }
}
