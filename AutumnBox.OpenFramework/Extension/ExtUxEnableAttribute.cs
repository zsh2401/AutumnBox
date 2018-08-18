/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 18:45:14 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 是否需要在运行时显示ExtensionRunningWindow
    /// </summary>
    public class ExtUxEnableAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtUxEnableAttribute(bool value) : base(value)
        {
        }
        /// <summary>
        /// 默认构造,值为true
        /// </summary>
        public ExtUxEnableAttribute() : base(true)
        {
        }
    }
}
