using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块是否是开发人员模式
    /// </summary>
    public class ExtDeveloperMode : SingleInfoAttribute
    {
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ExtensionInformationKeys.IS_DEVELOPING;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtDeveloperMode(bool value) : base(value)
        {
        }
    }
}
