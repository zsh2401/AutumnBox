/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 21:30:23 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本Desotry方法参数
    /// </summary>
    public class ScriptDestoryArgs:IScriptArgs
    {
        /// <summary>
        /// 获取脚本本身，类似this
        /// </summary>
        public ScriptBase Self { get; internal set; }
        /// <summary>
        /// 获取该脚本的对应上下文
        /// </summary>
        public Context Context { get; private set; }
        internal ScriptDestoryArgs(ScriptBase self)
        {
            this.Self = self;
            this.Context = self;
        }
    }
}
