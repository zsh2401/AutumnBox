/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 15:57:24
** filename: CoffeExecuterResult.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class CommandExecuterResult:IShellOutput
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int ExitCode { get; private set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return ExitCode == 0;
            }
        }
        /// <summary>
        /// 执行期间所有的输出
        /// </summary>
        public OutputData Output { get; private set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="output"></param>
        /// <param name="exitCode"></param>
        internal CommandExecuterResult(OutputData output, int exitCode)
        {
            Output = output;
            ExitCode = exitCode;
        }
        /// <summary>
        /// 转换成ShellOutput
        /// </summary>
        /// <returns></returns>
        public ShellOutput ToShellOutput()
        {
            return new ShellOutput(Output) { ReturnCode = ExitCode };
        }

        public OutputData ToOutputData()
        {
            var result = new OutputData();
            result.Append(Output);
            return result;
        }

        public void PrintOnLog(bool printOnRelease = false)
        {
            if (printOnRelease) {
                Logger.T($"PrintOnLog(): ExitCode: {ExitCode} Output: {Output}");
            } else {
                Logger.D($"PrintOnLog(): ExitCode: {ExitCode} Output: {Output}");
            }
        }

        public void PrintOnConsole()
        {
            Console.WriteLine($"PrintOnConsole(): ExitCode: {ExitCode} Output: {Output}");
        }

        /// <summary>
        /// CommandExecuterResult -> ShellOutput
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ShellOutput(CommandExecuterResult value)
        {
            return value.ToShellOutput();
        }
    }
}
