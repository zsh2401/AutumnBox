/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:18:35 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行所需的最低秋之盒API
    /// </summary>
    public class ExtMinApiAttribute : SingleInfoAttribute
    {
        /// <summary>
        /// 使用标准key
        /// </summary>
        public override string Key => ExtensionInformationKeys.MIN_ATMB_API;
        /// <summary>
        /// 默认构造
        /// </summary>
        /// <param name="value"></param>
        public ExtMinApiAttribute(int value) : base(value)
        {
        }
        internal ExtMinApiAttribute() : base(BuildInfo.API_LEVEL) { }
    }
}
