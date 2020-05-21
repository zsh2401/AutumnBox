/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:19:49 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块的目标API
    /// </summary>
    public class ExtTargetApiAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtTargetApiAttribute(int value) :
            base(ExtensionMetadataKeys.TARGET_ATMB_API, value)
        { }


        internal ExtTargetApiAttribute() :
            base(ExtensionMetadataKeys.TARGET_ATMB_API, BuildInfo.API_LEVEL)
        { }
    }
}
