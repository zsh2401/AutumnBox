/*

* ==============================================================================
*
* Filename: CoreLoggerBase
* Description: 
*
* Version: 1.0
* Created: 2020/4/26 2:07:19
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 核心日志器的基础
    /// </summary>
    public abstract class CoreLoggerBase : ICoreLogger
    {
        /// <summary>
        /// 已经记录过的日志
        /// </summary>
        public ILogsCollection Logs => LogsInner;

        /// <summary>
        /// 内部的Logs实现
        /// </summary>
        protected LogsCollection LogsInner { get; set; } = new LogsCollection();

        /// <summary>
        /// ILogsCollection的实现
        /// </summary>
        protected class LogsCollection : ObservableCollection<ILog>, ILogsCollection { }

        private object _threadSafeLogsLock = new object();

        /// <summary>
        /// 线程安全地对Logs进行操作
        /// </summary>
        /// <param name="action"></param>
        protected void ThreadSafeOperateLogsInner(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            lock (_threadSafeLogsLock)
            {
                action();
            }
        }

        /// <summary>
        /// 异步地,线程安全地操作LogsInner
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task ThreadSafeOperateLogsInnerAsync(Action action)
        {
            await Task.Run(() =>
            {
                ThreadSafeOperateLogsInner(action);
            });
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="args"></param>
        public abstract void Initialize(ICoreLoggerInitializeArgs args);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="log"></param>
        public abstract void Log(ILog log);

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 释放后发生
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 虚释放函数
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                LogsInner = null;
                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 终结器
        /// </summary>
        ~CoreLoggerBase()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        /// <summary>
        /// 释放函数
        /// </summary>
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
