/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 2:28:36 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.DPCommand
{
    /// <summary>
    /// 标准命令接口
    /// </summary>
    public interface ICommand : IDisposable, INotifyOutput
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <returns></returns>
        ICommandResult Execute();
    }
}
