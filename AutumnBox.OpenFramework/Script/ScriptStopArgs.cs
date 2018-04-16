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
    public class ScriptStopArgs:IScriptArgs
    {
        public Script Self { get; internal set; }
        public Context Context { get; internal set; }
        public ScriptStopArgs(Script self)
        {
            this.Context = self;
            this.Self = self;
        }
    }
}
