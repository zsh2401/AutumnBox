/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 21:30:23 (UTC +8:00)
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
    /// 脚本Desotry方法参数
    /// </summary>
    public class ScriptDestoryArgs:IScriptArgs
    {
        public Script Self { get; internal set; }
        public Context Context { get; private set; }
        public ScriptDestoryArgs(Script self)
        {
            this.Self = self;
            this.Context = self;
        }
    }
}
