#nullable enable
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace AutumnBox.Basic
{
    public abstract class StandardAdbManager : IAdbManager
    {
        protected HashSet<ICommandProcedureManager>? NotDisposedCpmSet;
        public DirectoryInfo AdbClientDirectory { get; }

        public IPEndPoint ServerEndPoint { get; }

        private readonly object _lock = new object();

        public StandardAdbManager()
        {
            NotDisposedCpmSet = new HashSet<ICommandProcedureManager>();
            AdbClientDirectory = InitializeClientFiles();
            var oldEnvPath = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", $"{AdbClientDirectory};{oldEnvPath}");
            ServerEndPoint = InitializeServer();
            Environment.SetEnvironmentVariable("ANDROID_ADB_SERVER_PORT", ServerEndPoint.Port.ToString());
        }

        protected abstract DirectoryInfo InitializeClientFiles();
        protected abstract IPEndPoint InitializeServer();

        protected virtual void StopServer()
        {
            lock (_lock)
            {
                using var cmd = new MyCommandProcedure("adb.exe", (ushort)ServerEndPoint.Port, AdbClientDirectory, $"-P{ServerEndPoint.Port} kill-server");
                cmd.Execute();
            }
        }

        public virtual ICommandProcedureManager OpenCommandProcedureManager()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(StandardAdbManager));
            }
            var cpm = new LocalProcedureManager(AdbClientDirectory, (ushort)ServerEndPoint.Port);
            NotDisposedCpmSet.Add(cpm);
            cpm.Disposed += Cpm_Disposed;
            return cpm;
        }

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

                StopServer();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~StandardAdbManager()
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
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
