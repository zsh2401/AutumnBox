#nullable enable
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
        private HashSet<ICommandProcedure>? notDisposeds;

        /// <summary>
        /// 构建基础的命令进程管理器
        /// </summary>
        /// <param name="adbClientDir"></param>
        /// <param name="adbPort"></param>
        public ProcedureManager(DirectoryInfo? adbClientDir = null, ushort adbPort = 6605)
        {
            notDisposeds = new HashSet<ICommandProcedure>();
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

        /// <summary>
        /// 打开新的命令
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ICommandProcedure OpenCommand(string commandName, params string[] args)
        {
            lock (openCommandLock)
            {
                var commandProcedure = new CommandProcedure(commandName, args);
                commandProcedure.Disposed += CommandProcedure_Disposed;
                notDisposeds!.Add(commandProcedure);
                return commandProcedure;
            }
        }

        private void CommandProcedure_Disposed(object sender, EventArgs e)
        {
            if (sender is ICommandProcedure cp)
            {
                cp.Disposed -= CommandProcedure_Disposed;
                notDisposeds?.Remove(cp);
            }
        }

        #region IDisposable Support
        /// <summary>
        /// 指示是否被释放
        /// </summary>
        public bool DisposedValue { get; private set; } = false;

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
                    var _cps = this.notDisposeds;
                    this.notDisposeds = null;
                    _cps.All((p) =>
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
                    _cps!.Clear();
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
