/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 4:02:34 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块名
    /// </summary>
    public class ExtNameAttribute : ExtInfoI18NAttribute
    {
        /// <summary>
        /// 默认key值
        /// </summary>
        public const string DEFAULT_KEY = "Name";
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pairsOfRegionsAndValue"></param>
        /// <param name="values"></param>
        public ExtNameAttribute(params string[] pairsOfRegionsAndValue):base(pairsOfRegionsAndValue)
        {
        }
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => DEFAULT_KEY;
    }
}
