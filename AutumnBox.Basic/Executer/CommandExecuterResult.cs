/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 15:57:24
** filename: CoffeExecuterResult.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class CommandExecuterResult
    {
        public int ExitCode { get; private set; }
        public bool IsSuccessful
        {
            get
            {
                return ExitCode == 0;
            }
        }
        public OutputData Output { get; private set; }
        internal CommandExecuterResult(OutputData output, int exitCode)
        {
            Output = output;
            ExitCode = exitCode;
        }
        public ShellOutput ToShellOutput()
        {
            return new ShellOutput(Output) { ReturnCode = ExitCode };
        }
    }
}
