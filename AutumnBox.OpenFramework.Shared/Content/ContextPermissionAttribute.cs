/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:18:26 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// Context权限标记
    /// </summary>
#if SDK
    internal
#else
    public
#endif
        class ContextPermissionAttribute : Attribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public CtxPer Value { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="per"></param>
        public ContextPermissionAttribute(CtxPer per)
        {
            this.Value = per;
        }
    }
}
