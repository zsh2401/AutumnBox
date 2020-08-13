#nullable enable
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Basic.Util;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 标准的ADB管理器
    /// </summary>
    public abstract class StandardAdbManager : IAdbManager
    {
        /// <summary>
        /// 内部维护尚未被析构的CPM
        /// </summary>
        protected HashSet<ICommandProcedureManager>? OpeningDpmSet { get; set; }

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
            OpeningDpmSet = new HashSet<ICommandProcedureManager>();
            AdbClientDirectory = InitializeClientFiles();
            ServerEndPoint = StartServer();
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
        protected virtual IPEndPoint StartServer(ushort port = 6605)
        {
            using var cmd = new CommandProcedure()
            {
                FileName = AdbExecutableFile.ToString(),
                Arguments = $"-P{port} start-server",
                DirectExecute = true,
            };
            cmd.InitializeAdbEnvironment(AdbClientDirectory, port);

            int line = 0;
            cmd.OutputReceived += (s, e) =>
            {
                line++;
                SLogger.Info(this, $"statring adb server {line}:{e.Text}");
            };
            cmd.Execute();
            return new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        }

        /// <summary>
        /// 杀死服务器
        /// </summary>
        protected virtual void KillServer()
        {
            lock (concurrentLock)
            {

                using var cmd = new CommandProcedure()
                {
                    FileName = AdbExecutableFile.ToString(),
                    Arguments = $"-P{ServerEndPoint.Port} kill-server",
                    DirectExecute = true,
                };
                cmd.InitializeAdbEnvironment(AdbClientDirectory, (ushort)ServerEndPoint.Port);

                new LocalAdbServerKiller(cmd).Kill().Wait();

                SLogger.Info(this, "Adb Server has been stopped.");
            }
        }

        /// <summary>
        /// 打开一个新的CPM
        /// </summary>
        /// <returns></returns>
        public ICommandProcedureManager OpenCommandProcedureManager()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(StandardAdbManager));
            }
            lock (concurrentLock)
            {
                var cpm = new ProcedureManager(AdbClientDirectory, (ushort)ServerEndPoint.Port);
                OpeningDpmSet?.Add(cpm);
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
                OpeningDpmSet?.Remove(cpm);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event EventHandler? Disposed;

        /// <summary>
        /// 析构
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                var _notDisposedCpm = OpeningDpmSet;
                OpeningDpmSet = null;
                if (disposing)
                {
                    foreach (var cpm in _notDisposedCpm)
                    {
                        try
                        {
                            cpm.Dispose();
                        }
                        catch (Exception e)
                        {
                            SLogger<StandardAdbManager>.CDebug("can not dispose", e);
                        }
                    }
                }
                _notDisposedCpm!.Clear();
                KillServer();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
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
