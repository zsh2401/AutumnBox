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
        /// Dft key
        /// </summary>
        public const string DEFAULT_KEY = "Auth";
        public override string Key => DEFAULT_KEY;
        /// <summary>
        /// 构建
        /// </summary>
        public ExtAuthAttribute(params string[] pairsOfRegionAndValue) : base(pairsOfRegionAndValue) { }
    }
}
