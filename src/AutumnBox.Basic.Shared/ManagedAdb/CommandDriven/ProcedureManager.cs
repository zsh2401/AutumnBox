#nullable enable
using AutumnBox.Basic.Data;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 基础的命令进程管理器
    /// </summary>
    public class ProcedureManager : ICommandProcedureManager, INotifyDisposed
    {
        /// <summary>
        /// 内部维护尚未被析构的cp
        /// </summary>
        readonly HashSet<ICommandProcedure> notDisposedSet = new HashSet<ICommandProcedure>();

        /// <summary>
        /// 构建基础的命令进程管理器
        /// </summary>
        /// <param name="adbClientDir"></param>
        /// <param name="adbPort"></param>
        public ProcedureManager(DirectoryInfo? adbClientDir = null, ushort adbPort = 6605)
        {
            this.adbClientDir = adbClientDir ?? new DirectoryInfo(".");
            this.adbPort = adbPort;
        }

        /// <summary>
        /// 析构完成事件
        /// </summary>
        public event EventHandler? Disposed;

        private readonly object openCommandLock = new object();
        private readonly DirectoryInfo adbClientDir;
        private readonly ushort adbPort;

        /// <inheritdoc/>
        public ICommandProcedure OpenCommand(string commandName, params string[] args)
        {
            lock (openCommandLock)
            {
                var commandProcedure = new CommandProcedure()
                {
                    FileName = commandName,
                    Arguments = string.Join(" ", args)
                };
                commandProcedure.InitializeAdbEnvironment(this.adbClientDir, this.adbPort);
                commandProcedure.Disposed += CommandProcedure_Disposed;
                lock (notDisposedSet)
                {
                    notDisposedSet?.Add(commandProcedure);
                }
                return commandProcedure;
            }
        }

        private void CommandProcedure_Disposed(object sender, EventArgs e)
        {
            if (handleDisposedEvent && sender is ICommandProcedure cp)
            {
                cp.Disposed -= CommandProcedure_Disposed;
                lock (notDisposedSet)
                {
                    notDisposedSet.Remove(cp);
                }
            }
        }

        #region IDisposable Support
        /// <summary>
        /// 指示是否被释放
        /// </summary>
        public bool DisposedValue { get; private set; } = false;

        private bool handleDisposedEvent = true;
        /// <summary>
        /// 释放函数内部实现
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    handleDisposedEvent = false;
                    notDisposedSet.All((p) =>
                     {
                         try
                         {
                             p.Dispose();
                         }
                         catch (Exception e)
                         {
                             SLogger<CommandProcedure>.CDebug("can not dispose command procedure", e);
                         }
                         return true;
                     });
                    this.notDisposedSet.Clear();
                    // TODO: 释放托管状态(托管对象)。
                }
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                DisposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// ~
        /// </summary>
        ~ProcedureManager()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        /// <summary>
        /// 析构接口
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
