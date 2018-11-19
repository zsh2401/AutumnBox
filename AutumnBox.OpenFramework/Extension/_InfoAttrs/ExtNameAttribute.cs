/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:02:34 (UTC +8:00)
** desc： ...
*************************************************/


namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块名
    /// </summary>
    public class ExtNameAttribute : ExtInfoI18NAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pairsOfRegionsAndValue"></param>
        public ExtNameAttribute(params string[] pairsOfRegionsAndValue) : base(pairsOfRegionsAndValue)
        {
        }
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ExtensionInformationKeys.NAME;
    }
}
