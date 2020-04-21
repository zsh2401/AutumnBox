#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 基础的命令进程管理器
    /// </summary>
    public class LocalProcedureManager : ICommandProcedureManager
    {
        private HashSet<ICommandProcedure>? commandProcedures;

        public LocalProcedureManager(DirectoryInfo? adbClientDir = null, ushort adbPort = 6605)
        {
            commandProcedures = new HashSet<ICommandProcedure>();
            this.adbClientDir = adbClientDir ?? new DirectoryInfo(".");
            this.adbPort = adbPort;
        }

        private readonly object openCommandLock = new object();
        private readonly DirectoryInfo adbClientDir;
        private readonly ushort adbPort;

        public ICommandProcedure OpenCommand(string commandName, params string[] args)
        {
            lock (openCommandLock)
            {
                var commandProcedure = new MyCommandProcedure(commandName, adbPort, adbClientDir, args);
                commandProcedure.Finished += (s, e) =>
                {
                    commandProcedures!.Remove((MyCommandProcedure)s);
                };
                commandProcedures!.Add(commandProcedure);
                return commandProcedure;
            }
        }

        #region IDisposable Support

        public bool Disposed { get; private set; } = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    commandProcedures.All((p) =>
                    {
                        p.Dispose();
                        return true;
                    });
                    // TODO: 释放托管状态(托管对象)。
                }

                commandProcedures!.Clear();
                commandProcedures = null;
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                Disposed = true;
            }
        }

        //TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~LocalProcedureManager()
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
