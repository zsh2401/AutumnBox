#nullable enable
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AutumnBox.Basic
{
    /// <summary>
    /// 标准的ADB管理器
    /// </summary>
    public abstract class StandardAdbManager : IAdbManager
    {
        /// <summary>
        /// 内部维护尚未被析构的CPM
        /// </summary>
        protected HashSet<ICommandProcedureManager>? NotDisposedCpmSet { get; set; }

        /// <summary>
        /// ADB客户端文件夹
        /// </summary>
        public DirectoryInfo AdbClientDirectory { get; }

        /// <summary>
        /// ADB服务器终端点
        /// </summary>
        public IPEndPoint ServerEndPoint { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual FileInfo AdbExecutableFile
        {
            get
            {
                return new FileInfo(Path.Combine(AdbClientDirectory.FullName, "adb.exe"));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual FileInfo FastbootExecutableFile
        {
            get
            {
                return new FileInfo(Path.Combine(AdbClientDirectory.FullName, "fastboot.exe"));
            }
        }

        private readonly object concurrentLock = new object();

        /// <summary>
        /// 构建标准ADB管理器
        /// </summary>
        public StandardAdbManager()
        {
            NotDisposedCpmSet = new HashSet<ICommandProcedureManager>();
            AdbClientDirectory = InitializeClientFiles();
            var oldEnvPath = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", $"{AdbClientDirectory};{oldEnvPath}");
            ServerEndPoint = StartServer();
            Environment.SetEnvironmentVariable("ANDROID_ADB_SERVER_PORT", ServerEndPoint.Port.ToString());
        }

        /// <summary>
        /// 初始化客户端文件
        /// </summary>
        /// <returns></returns>
        protected abstract DirectoryInfo InitializeClientFiles();

        /// <summary>
        /// 初始化服务器
        /// </summary>
        /// <returns></returns>
        protected abstract IPEndPoint StartServer();

        /// <summary>
        /// 杀死服务器
        /// </summary>
        protected virtual void KillServer()
        {
            lock (concurrentLock)
            {
                using var cmd = new CommandProcedure(AdbExecutableFile.ToString(), $"-P{ServerEndPoint.Port} kill-server");
                int line = 0;
                cmd.OutputReceived += (s, e) =>
                {
                    line++;
                    SLogger.Info(this, $"killing adb server {line}:{e.Text}");
                };
                cmd.Execute();
                SLogger.Info(this, "server killed");
            }
        }

        /// <summary>
        /// 打开一个新的CPM
        /// </summary>
        /// <returns></returns>
        public ICommandProcedureManager OpenCommandProcedureManager()
        {
            lock (concurrentLock)
            {
                if (disposedValue)
                {
                    throw new ObjectDisposedException(nameof(StandardAdbManager));
                }
                var cpm = new ProcedureManager(AdbClientDirectory, (ushort)ServerEndPoint.Port);
                NotDisposedCpmSet?.Add(cpm);
                cpm.Disposed += Cpm_Disposed;
                return cpm;
            }
        }

        /// <summary>
        /// 处理某个CPM被析构的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cpm_Disposed(object sender, EventArgs e)
        {
            if (sender is ICommandProcedureManager cpm)
            {
                cpm.Disposed -= Cpm_Disposed;
                NotDisposedCpmSet?.Remove(cpm);
            }
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
                }
                var _notDisposedCpm = NotDisposedCpmSet;
                NotDisposedCpmSet = null;
                foreach (var cpm in _notDisposedCpm)
                {
                    try
                    {
                        SLogger<StandardAdbManager>.CDebug("disposing");
                        cpm.Dispose();
                        SLogger<StandardAdbManager>.CDebug("disposed");
                    }
                    catch (Exception e)
                    {
                        SLogger<StandardAdbManager>.CDebug("can not dispose", e);
                    }
                }
                _notDisposedCpm.Clear();

                KillServer();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                disposedValue = true;
            }
        }

        /// <summary>
        /// 终结函数
        /// </summary>
        ~StandardAdbManager()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        /// <summary>
        /// 接口实现
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
