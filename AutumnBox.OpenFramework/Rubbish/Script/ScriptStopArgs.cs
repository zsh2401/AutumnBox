/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 20:34:06 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本停止方法参数
    /// </summary>
    public class ScriptStopArgs:IScriptArgs
    {
        /// <summary>
        /// 获取脚本本身，类似this
        /// </summary>
        public ScriptBase Self { get; internal set; }
        /// <summary>
        /// 获取该脚本的对应上下文
        /// </summary>
        public Context Context { get; private set; }
        internal ScriptStopArgs(ScriptBase self)
        {
            this.Context = self;
            this.Self = self;
        }
    }
}
