/*************************************************
** auth： zsh2401@163.com
** date:  2018/11/12 12:28:26 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Exceptions
{
    /// <summary>
    /// 命令失败异常
    /// </summary>
    public class CommandFailedException : Exception
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int? ExitCode { get; } = null;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="output"></param>
        /// <param name="exitCode"></param>
        public CommandFailedException(string output, int? exitCode=null) : base(output)
        {
            ExitCode = exitCode;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="output"></param>
        /// <param name="exitCode"></param>
        public CommandFailedException(Output output, int? exitCode=null)
            : this(output.ToString(), exitCode)
        {
        }
    }
}
