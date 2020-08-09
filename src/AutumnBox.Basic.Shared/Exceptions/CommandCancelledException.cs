/*

* ==============================================================================
*
* Filename: CommandCancelledException
* Description: 
*
* Version: 1.0
* Created: 2020/8/9 19:04:13
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Exceptions
{
    /// <summary>
    /// 表明命令被外部终止的异常
    /// </summary>
    public class CommandCancelledException : Exception
    {
        /// <summary>
        /// 构造表明命令被外部终止的异常
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <param name="recordOutput"></param>
        public CommandCancelledException(string? fileName = null, string? arguments = null, Output? recordOutput = null) : base($"Comamnd cancelled {fileName} {arguments}")
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            RecordOutput = recordOutput ?? throw new ArgumentNullException(nameof(recordOutput));
        }

        /// <summary>
        /// 命令文件名
        /// </summary>
        public string? FileName { get; }

        /// <summary>
        /// 命令参数
        /// </summary>
        public string? Arguments { get; }

        /// <summary>
        /// 已经记录的输出内容
        /// </summary>
        public Output? RecordOutput { get; }
    }
}
