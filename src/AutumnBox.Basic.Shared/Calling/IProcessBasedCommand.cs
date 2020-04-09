/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 8:58:27 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 基于进程的命令
    /// </summary>
    public interface IProcessBasedCommand : IDisposable,INotifyOutput,IReceiveOutputByTo<IProcessBasedCommand>
    {
        /// <summary>
        /// 执行
        /// </summary>
        IProcessBasedCommandResult Execute();
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <returns></returns>
        Task<IProcessBasedCommandResult> ExecuteAsync();
        /// <summary>
        /// 杀死
        /// </summary>
        void Kill();
    }
}
