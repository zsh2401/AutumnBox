/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/12 10:50:39 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// DO NOT USE THIS
    /// </summary>
#if !SDK
    public
#else
    internal
#endif
        class ExtHighPermissionAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value"></param>
        public ExtHighPermissionAttribute(bool value) : base(value)
        {
        }
    }
}
