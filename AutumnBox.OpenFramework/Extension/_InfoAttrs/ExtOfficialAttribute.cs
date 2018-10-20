/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 14:26:17 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 官方拓展
    /// </summary>
#if SDK
    internal
#else
    public
#endif
    sealed class ExtOfficialAttribute : SingleInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtOfficialAttribute(bool value) : base(value)
        {
        }
    }
}
