/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:06:51 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块所有者特性
    /// </summary>
    public class ExtAuthAttribute : ExtInfoI18NAttribute
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="auth"></param>
        public ExtAuthAttribute(string auth) : base(auth) { }
    }
}
