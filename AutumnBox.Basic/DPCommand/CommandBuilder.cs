/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 3:36:16 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.DPCommand
{
    /// <summary>
    /// 命令构造器
    /// </summary>
    public class CommandBuilder
    {
        /// <summary>
        /// 参数
        /// </summary>
        protected List<string> Arguments { get; set; } = new List<string>();
        /// <summary>
        /// 执行文件
        /// </summary>
        protected string FileName { get; set; } = "cmd.exe";
        /// <summary>
        /// 清空参数
        /// </summary>
        public virtual void ClearArgs()
        {
            Arguments.Clear();
        }
        /// <summary>
        /// 设置执行文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual CommandBuilder File(string fileName)
        {
            this.FileName = fileName;
            return this;
        }
        public virtual CommandBuilder ArgWithDoubleQuotation(string arg)
        {
            Arg("\"" + arg + "\"");
        }
        public virtual CommandBuilder Arg(string arg)
        {
            Arguments.Add(arg);
            return this;
        }
        public virtual ICommand ToCommand()
        {
            return new ProcessBasedCommand(FileName, string.Join(" ", Arguments.ToArray()));
        }
        public override string ToString()
        {
            return FileName + " " + string.Join(" ", Arguments.ToArray());
        }
    }
}
