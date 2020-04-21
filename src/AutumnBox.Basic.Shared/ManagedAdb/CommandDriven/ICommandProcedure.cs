#nullable enable
using AutumnBox.Basic.Data;
using System.Threading.Tasks;
using System;
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
        /// <returns></returns>
        ICommandResult Execute();

        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效,如命令已经在执行</exception>
        /// <returns></returns>
        Task<ICommandResult> ExecuteAsync();

        /// <summary>
        /// 取消执行
        /// </summary>
        /// <exception cref="InvalidOperationException">事务状态异常</exception>
        void Cancel();

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <exception cref="InvalidOperationException">事务状态异常</exception>
        ICommandResult Result { get; }

        /// <summary>
        /// 获取发生的错误
        /// </summary>
        /// <exception cref="InvalidOperationException">事务状态异常</exception>
        Exception? Exception { get; }
    }
}
