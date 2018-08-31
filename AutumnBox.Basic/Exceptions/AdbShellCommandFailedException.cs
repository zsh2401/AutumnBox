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
    public class AdbShellCommandFailedException : Exception
    {
        public Int32 ExitCode { get; private set; }
        public AdbShellCommandFailedException(int exitCode, string output) : base(output)
        {
            ExitCode = exitCode;
        }
    }
}
