/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:47:51 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Exceptions
{
    /// <summary>
    /// adb shell command执行失败异常
    /// </summary>
    public class AdbShellCommandFailedException : Exception
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public Int32 ExitCode { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="output"></param>
        public AdbShellCommandFailedException(int exitCode, string output) : base(output)
        {
            ExitCode = exitCode;
        }
    }
}
