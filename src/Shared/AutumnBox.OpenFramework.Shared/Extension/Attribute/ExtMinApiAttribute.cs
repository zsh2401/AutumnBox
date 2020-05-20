/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:18:35 (UTC +8:00)
** desc： ...
*************************************************/

using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行所需的最低秋之盒API
    /// </summary>
    public class ExtMinApiAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        /// <param name="value"></param>
        public ExtMinApiAttribute(int value) :
            base(ExtensionMetadataKeys.MIN_ATMB_API, value)
        {
        }

        internal ExtMinApiAttribute() :
            base(ExtensionMetadataKeys.MIN_ATMB_API, BuildInfo.API_LEVEL)
        { }
    }
}
