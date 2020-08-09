#nullable enable
using AutumnBox.Basic.Data;
using System.Threading.Tasks;
using System;
using AutumnBox.Basic.Exceptions;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 命令事务进程
    /// </summary>
    public interface ICommandProcedure : INotifyOutput, IDisposable, INotifyDisposed
    {
        /// <summary>
        /// 获取状态
        /// </summary>
        CommandStatus Status { get; }

        /// <summary>
        /// 命令执行完毕
        /// </summary>
        event EventHandler Finished;

        /// <summary>
        /// 命令开始执行
        /// </summary>
        event EventHandler Executing;

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效,如命令已经在执行</exception>
        /// <exception cref="ObjectDisposedException">已经被释放</exception>
        /// <exception cref="CommandCancelledException">命令被外部终止</exception>
        /// <returns></returns>
        CommandResult Execute();

        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效,如命令已经在执行</exception>
        /// <exception cref="ObjectDisposedException">已经被释放</exception>
        /// <exception cref="CommandCancelledException">命令被外部终止</exception>
        /// <returns></returns>
        Task<CommandResult> ExecuteAsync();

        /// <summary>
        /// 取消执行
        /// </summary>
        /// <exception cref="InvalidOperationException">事务状态异常</exception>
        /// <exception cref="ObjectDisposedException">对象已经被释放</exception>
        void Cancel();

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <exception cref="InvalidOperationException">事务暂未完成,没有结果</exception>
        CommandResult Result { get; }

        /// <summary>
        /// 获取内部发生的错误
        /// </summary>
        Exception? Exception { get; }
    }
}
