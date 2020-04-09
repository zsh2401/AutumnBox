using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 标准的CommandExecutor命令执行器
    /// </summary>
    public interface ICommandExecutor : IDisposable,INotifyOutput
    {
        /// <summary>
        /// 当CommandExecutor被析构时触发
        /// </summary>
        event EventHandler Disposed;
        /// <summary>
        /// 当一条命令开始执行时触发
        /// </summary>
        event CommandExecutingEventHandler CommandExecuting;
        /// <summary>
        /// 当一条命令完成时触发
        /// </summary>
        event CommandExecutedEventHandler CommandExecuted;
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        ICommandResult Execute(string fileName, string args);
        /// <summary>
        /// 取消执行当前执行的命令
        /// </summary>
        void CancelCurrent();
    }
}
