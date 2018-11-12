/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:47:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
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
    public class AdbShellCommandFailedException : CommandErrorException
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="output"></param>
        public AdbShellCommandFailedException(string output, int exitCode)
            : base(output, exitCode)
        {
        }
    }
}
