/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:36:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Exceptions
{
    /// <summary>
    /// ADB命令执行失败异常
    /// </summary>
    public class AdbCommandFailedException : Exception
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int ExitCode { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public AdbCommandFailedException()
            : base() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="output"></param>
        public AdbCommandFailedException(Output output)
            : base(output.ToString())
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public AdbCommandFailedException(string message)
            : base(message)
        {
        }
    }
}
