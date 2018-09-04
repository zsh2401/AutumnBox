/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:08:24 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.Basic.Calling.BuilderSchema
{
    /// <summary>
    /// 参数构造器接口
    /// </summary>
    public interface IArgBuilder
    {
        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        IArgBuilder Arg(string arg);
        IArgBuilder ArgWithDoubleQuotation(string arg);
        IProcessBasedCommand ToCommand();
    }
}
