/* =============================================================================*\
*
* Filename: ShellOutput
* Description: 
*
* Version: 1.0
* Created: 2017/11/21 17:15:25 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 用于保存AndroidShell类的返回值
    /// </summary>
    public class ShellOutput : IShellOutput
    {
        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool IsSuccessful { get { return ExitCode == 0; } }
        public int ReturnCode { get; set; } = 0;
        /// <summary>
        /// 返回值
        /// </summary>
        public int ExitCode => ReturnCode;
        /// <summary>
        /// 添加输出
        /// </summary>
        /// <param name="text"></param>
        public void AppendOut(string text)
        {
            allSb.Append(text);
        }

        public Output ToOutputData()
        {
            return Output.From(this);
        }

        public void PrintOnLog(bool printOnRelease = false)
        {
            if (printOnRelease)
            {
                Logger.T($"PrintOnLog(): ExitCode: {ExitCode} Output: {All}");
            }
            else
            {
                Logger.D($"PrintOnLog(): ExitCode: {ExitCode} Output: {All}");
            }
        }

        public void PrintOnConsole()
        {
            Console.WriteLine($"PrintOnConsole(): ExitCode: {ExitCode} Output: {All}");
        }

        /// <summary>
        /// 每一行输出
        /// </summary>
        public string[] LineAll
        {
            get
            {
                return All.Split(Environment.NewLine.ToCharArray());
            }
        }
        /// <summary>
        /// 所有输出
        /// </summary>
        public string All
        {
            get
            {
                return allSb.ToString();
            }
        }
        private StringBuilder allSb = new StringBuilder();
        /// <summary>
        /// 确保只有AutumnBox.Basic可以构造这个类
        /// </summary>
        internal ShellOutput() { }
        /// <summary>
        /// 确保只有AutumnBox.Basic可以构造这个类
        /// </summary>
        internal ShellOutput(Output o)
        {
            allSb.Append(o.All);
        }
    }
}
