/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 21:47:57 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.OpenFramework.Script
{
    public class ScriptInitArgs:IScriptArgs
    {
        /// <summary>
        /// 获取脚本本身，类似this
        /// </summary>
        public ScriptBase Self { get; internal set; }
        /// <summary>
        /// 获取该脚本的对应上下文
        /// </summary>
        public Context Context { get; private set; }
        internal ScriptInitArgs(ABEScript self)
        {
            this.Self = self;
            this.Context = self;
        }
    }
}
