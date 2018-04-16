/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 21:47:57 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Script
{
    public class ScriptInitArgs:IScriptArgs
    {
        public Script Self { get; internal set; }
        public Context Context { get; private set; }
        public ScriptInitArgs(ABEScript self)
        {
            this.Self = self;
            this.Context = self;
        }
    }
}
