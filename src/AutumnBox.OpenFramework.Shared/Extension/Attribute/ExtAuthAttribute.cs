/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:06:51 (UTC +8:00)
** desc： ...
*************************************************/

using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块所有者特性
    /// </summary>
    public class ExtAuthAttribute : ExtInfoI18NAttribute
    {

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ExtensionInformationKeys.AUTH;
        /// <summary>
        /// 构建
        /// </summary>
        public ExtAuthAttribute(params string[] pairsOfRegionAndValue) : base(pairsOfRegionAndValue) { }
    }
}
