using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.ManagementV2
{
    /// <summary>
    /// 提供一个长期存在的Shell控制功能
    /// </summary>
    public class AndroidShell : IDisposable
    {
        private CommandExecutor executor;
        private IDevice device;
        /// <summary>
        /// 构造一个提供长久Shell的对象
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        public AndroidShell(CommandExecutor executor, IDevice device)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
            this.device = device ?? throw new ArgumentNullException(nameof(device));
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Open()
        {
            Task.Run(() =>
            {
                executor.AdbShell(device);
            });
        }
        /// <summary>
        /// 写一个字符
        /// </summary>
        /// <param name="_char"></param>
        public void Write(char _char)
        {
            executor.Write(_char);
        }
        /// <summary>
        /// 写一行字符
        /// </summary>
        /// <param name="str"></param>
        public void WriteLine(string str)
        {
            executor.WriteLine(str);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

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
                executor.KillCurrent();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                executor = null;
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
