/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 23:31:59 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 支持多语言的字符串特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ExtInfoI18NAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string Lang { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public override string Key
        {
            get
            {
                if (Lang == null)
                {
                    return base.Key;
                }
                else
                {
                    return base.Key + "_" + Lang;
                }
            }
        }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="value"></param>
        public ExtInfoI18NAttribute(string value) : base(value) { }
    }
}
