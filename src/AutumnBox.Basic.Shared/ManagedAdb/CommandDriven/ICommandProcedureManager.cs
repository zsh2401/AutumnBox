#nullable enable
using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 命令驱动器
    /// </summary>
    public interface ICommandProcedureManager : IDisposable, INotifyDisposed
    {
        /// <summary>
        /// 指示是否被释放
        /// </summary>
        bool DisposedValue { get; }

        /// <summary>
        /// 构建一个命令过程
        /// </summary>
        /// <param name="commandName">命令名</param>
        /// <param name="args">额外参数</param>
        /// <returns>命令过程</returns>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="ObjectDisposedException">对象已经被释放</exception>
        ICommandProcedure OpenCommand(string commandName, params string[] args);
    }
}
