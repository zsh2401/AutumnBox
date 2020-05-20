/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:02:34 (UTC +8:00)
** desc： ...
*************************************************/


namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块名标记
    /// </summary>
    public class ExtNameAttribute : ExtensionI18NTextInfoAttribute
    {
        /// <summary>
        /// 构造一个拓展模块名特性
        /// </summary>
        /// <param name="defaultText"></param>
        /// <param name="otherLanguageTexts"></param>
        public ExtNameAttribute(string defaultText, params string[] otherLanguageTexts) : base(defaultText, otherLanguageTexts)
        {
        }

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ExtensionMetadataKeys.NAME;
    }
}
