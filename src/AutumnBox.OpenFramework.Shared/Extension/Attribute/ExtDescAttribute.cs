/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:08:35 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块说明特性
    /// </summary>
    public class ExtDescAttribute : ExtensionI18NTextInfoAttribute
    {
        /// <summary>
        /// 拓展模块说明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defaultText"></param>
        /// <param name="other"></param>
        public ExtDescAttribute(string defaultText, params string[] other) : base(defaultText, other)
        {
        }

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ExtensionMetadataKeys.DESCRIPTION;
    }
}
