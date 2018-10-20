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
    public class ExtTargetApiAttribute : SingleInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtTargetApiAttribute(int value) : base(value)
        {
        }
        internal ExtTargetApiAttribute() : base(BuildInfo.API_LEVEL) { }
    }
}
