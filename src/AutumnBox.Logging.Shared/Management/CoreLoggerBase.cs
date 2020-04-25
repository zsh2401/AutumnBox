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
using AutumnBox.Basic.Data;
using System;
using System.Collections.ObjectModel;

namespace AutumnBox.Logging.Management
{
    abstract class CoreLoggerBase : ICoreLogger, INotifyDisposed
    {
        public ILogsCollection Logs => _logs;
        private LogsCollection _logs = new LogsCollection();
        private class LogsCollection : ObservableCollection<ILog>, ILogsCollection { }
        protected void ThreadSafeAdd(ILog log)
        {
            lock (_logs)
            {
                _logs.Add(log);
            }
        }
        protected void ThreadSafeResizeLogs()
        {
            lock (_logs)
            {
                _logs.Clear();
            }
        }
        protected Action<string> Writer { get; set; }
        public void Initialize(ICoreLoggerInitializeArgs args)
        {
            OnInitialize(args);
        }
        protected virtual void OnInitialize(ICoreLoggerInitializeArgs args)
        {
            Writer = args.Writer;
        }
        public void Log(ILog log)
        {
            HandleLog(log);
        }
        protected abstract void HandleLog(ILog log);
        protected virtual void DisposeManagedResource() { }
        protected virtual void DisposeUnmanagedResources() { }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        public event EventHandler Disposed;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeManagedResource();
                }
                DisposeUnmanagedResources();
                _logs = null;
                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~CoreLoggerBase()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
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
