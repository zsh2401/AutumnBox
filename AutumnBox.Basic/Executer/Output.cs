/* =============================================================================*\
*
* Filename: OutputData.cs
* Description: 
*
* Version: 1.0
* Created: 9/15/2017 16:01:48(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Executer
{
    using AutumnBox.Basic.Util;
    using AutumnBox.Support.CstmDebug;
    using System;

    public class Output : IPrintable
    {
        /// <summary>
        /// 所有的输出
        /// </summary>
        public string[] LineAll
        {
            get
            {
                return All.Split(Environment.NewLine.ToCharArray());
            }
        }
        /// <summary>
        /// 所有的标准输出
        /// </summary>
        public string[] LineOut
        {
            get
            {
                return Out.Split(Environment.NewLine.ToCharArray());
            }
        }
        /// <summary>
        /// 所有的标准错误
        /// </summary>
        public string[] LineError
        {
            get
            {
                return Error.Split(Environment.NewLine.ToCharArray());
            }
        }
        /// <summary>
        /// 所有的输出
        /// </summary>
        public string All { get; protected set; }
        /// <summary>
        /// 所有的标准输出
        /// </summary>
        public string Out { get; protected set; }
        /// <summary>
        /// 所有的标准错误
        /// </summary>
        public string Error { get; protected set; }

        /// <summary>
        /// 判断输出中是否包含某段字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public bool Contains(string str, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                return All.ToLower().Contains(str.ToLower());
            }
            else
            {
                return All.Contains(str);
            }
        }
        /// <summary>
        /// 添加另一个OutputData对象的内容
        /// </summary>
        /// <param name="output"></param>
        public void Append(Output output)
        {
            this.All += output.All;
            this.Out += output.Out;
            this.Error += output.Error;
        }
        /// <summary>
        /// 获取完整的输出数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return All.ToString();
        }
        public Output() {
            this.Out = "";
            this.Error = "";
            this.All = "";
        }
        public Output(string all, string _out, string err ="") {
            this.Out = _out;
            this.Error = err;
            this.All = all;
        }
        public void PrintOnLog(bool printOnRelease = false)
        {
            if (printOnRelease)
            {
                Logger.T($"PrintOnLog(): {ToString()}");
            }
            else
            {
                Logger.D($"PrintOnLog(): {ToString()}");
            }
        }
        public void PrintOnConsole()
        {
            Console.WriteLine($"PrintOnConsole(): {ToString()}");
        }
    }
}
