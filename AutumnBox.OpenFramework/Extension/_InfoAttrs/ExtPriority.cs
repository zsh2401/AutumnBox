/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 19:24:44 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块优先级
    /// </summary>
    public class ExtPriority : SingleInfoAttribute
    {
        /// <summary>
        /// 使用标准KEY
        /// </summary>
        public override string Key => ExtensionInformationKeys.PRIORITY;
        /// <summary>
        /// 构造拓展模块优先级信息特性
        /// </summary>
        /// <param name="value"></param>
        public ExtPriority(int value) : base(value)
        {
        }
    }
}
