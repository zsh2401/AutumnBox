namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 每一个脚本方法的参数都必须实现的接口
    /// </summary>
    public interface IScriptArgs
    {
        /// <summary>
        /// 获取脚本本身，类似this
        /// </summary>
        ScriptBase Self { get;}
        /// <summary>
        /// 获取该脚本的对应上下文
        /// </summary>
        Context Context { get; }
    }
}
