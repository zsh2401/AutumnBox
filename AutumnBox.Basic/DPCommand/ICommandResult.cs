using AutumnBox.Basic.Data;
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.DPCommand
{
    /// <summary>
    /// 命令执行结果
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// 输出
        /// </summary>
        Output Output { get; }
        /// <summary>
        /// 返回码
        /// </summary>
        int ExitCode { get; }
    }
}
