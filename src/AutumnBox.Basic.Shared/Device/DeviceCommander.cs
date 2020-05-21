#nullable enable
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 只要是向设备发出命令实现功能的类，理应该实现此类
    /// </summary>
    public abstract class DeviceCommander : IDisposable, INotifyOutput, INotifyDisposed
    {
        /// <summary>
        /// 接收到输出
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived
        {
            add
            {
                Executor.OutputReceived += value;
            }
            remove
            {
                Executor.OutputReceived -= value;
            }
        }

        /// <summary>
        /// 执行器
        /// </summary>
        protected ICommandExecutor Executor { get; }

        /// <summary>
        /// 目标设备
        /// </summary>
        public IDevice Device { get; private set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public DeviceCommander(IDevice device, ICommandExecutor? executor = null)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
            Executor = executor ?? new HestExecutor();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 对象被释放后
        /// </summary>
        public event EventHandler? Disposed;

        /// <summary>
        /// 可继承的释放函数
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
                Executor.Dispose();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 释放器
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
