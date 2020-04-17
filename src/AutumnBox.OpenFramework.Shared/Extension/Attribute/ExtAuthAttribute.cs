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
    public class ExtAuthAttribute : ExtensionI18NTextInfoAttribute
    {
        /// <summary>
        /// 模块所有者
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defaultText"></param>
        /// <param name="other"></param>
        public ExtAuthAttribute(string defaultText, params string[] other) : base(defaultText, other)
        {
        }

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ExtensionMetadataKeys.AUTH;
    }
}
