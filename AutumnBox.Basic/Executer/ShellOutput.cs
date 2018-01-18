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
    public class ShellOutput
    {
        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool IsSuccess { get { return ReturnCode == 0; } }
        /// <summary>
        /// 返回值
        /// </summary>
        public int ReturnCode { get; set; } = 0;
        /// <summary>
        /// 添加输出
        /// </summary>
        /// <param name="text"></param>
        public void OutAdd(string text)
        {
            LineAll.Add(text);
            All.AppendLine(text);
        }
        /// <summary>
        /// 每一行输出
        /// </summary>
        public List<string> LineAll { get; private set; } = new List<string>();
        /// <summary>
        /// 所有输出
        /// </summary>
        public StringBuilder All { get; private set; } = new StringBuilder();
        /// <summary>
        /// 确保只有AutumnBox.Basic可以构造这个类
        /// </summary>
        internal ShellOutput() { }
        /// <summary>
        /// 确保只有AutumnBox.Basic可以构造这个类
        /// </summary>
        internal ShellOutput(OutputData o)
        {
            LineAll = o.LineAll;
            All = o.All;
        }
        /// <summary>
        /// ShellOutput -> OutputData
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator OutputData(ShellOutput value)
        {
            var output = new OutputData();
            output.LineAll.AddRange(value.LineAll);
            output.All.Append(value.All);
            output.StopAdding();
            return output;
        }
    }
}
