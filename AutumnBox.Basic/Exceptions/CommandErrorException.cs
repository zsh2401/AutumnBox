/*************************************************
** auth： zsh2401@163.com
** date:  2018/11/12 12:28:26 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Exceptions
{
    /// <summary>
    /// 命令失败异常，所有命令相关的异常的基类
    /// </summary>
    [Serializable]
    public class CommandErrorException : Exception
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int? ExitCode { get; set; } = null;
        /// <summary>
        /// 构造
        /// </summary>
        public CommandErrorException() { }
        /// <summary>
        /// 构造
        /// </summary>
        public CommandErrorException(string message, int? exitCode = null) : base(message)
        {
            ExitCode = exitCode;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="output"></param>
        /// <param name="exitCode"></param>
        public CommandErrorException(Output output, int? exitCode = null) : base(output.ToString())
        {
            ExitCode = exitCode;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        /// <param name="output"></param>
        /// <param name="exitCode"></param>
        public CommandErrorException(string message, Output output, int? exitCode = null)
            : base($"{message}{Environment.NewLine}{output}")
        {
            ExitCode = exitCode;
        }
        /// <summary>
        /// 构造
        /// </summary>
        public CommandErrorException(string message, Exception inner, int? exitCode = null) : base(message, inner)
        {
            ExitCode = exitCode;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        /// <param name="output"></param>
        /// <param name="inner"></param>
        /// <param name="exitCode"></param>
        public CommandErrorException(string message, Output output, Exception inner, int? exitCode = null)
            : base($"{message}{Environment.NewLine}{output}",inner)
        {
            ExitCode = exitCode;
        }
        /// <summary>
        /// 构造
        /// </summary>
        protected CommandErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
