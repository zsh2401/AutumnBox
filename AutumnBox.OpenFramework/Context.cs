/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework
{
    /// <summary>
    /// AutumnBox开放框架上下文
    /// </summary>
    public abstract class Context
    {
        /// <summary>
        /// 标签,主要用于打log
        /// </summary>
        public virtual string Tag => this.GetType().Name;
        internal virtual ContextPermissionLevel GetPermissionLevel()
        {
            var assName = this.GetType().Assembly.GetName().Name;
            switch (assName)
            {
                case BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME:
                    return ContextPermissionLevel.Mid;
                case BuildInfo.AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME:
                    return ContextPermissionLevel.High;
                default:
                    return ContextPermissionLevel.Low;
            }
        }
        internal void PermissionCheck(ContextPermissionLevel minLevel = ContextPermissionLevel.Mid)
        {
            if ((int)GetPermissionLevel() < (int)minLevel)
            {
                throw new AccessDeniedException();
            }
        }
    }
    /// <summary>
    /// Context权限等级
    /// </summary>
    public enum ContextPermissionLevel
    {
        /// <summary>
        /// 最低权限
        /// </summary>
        Low = 0,
        /// <summary>
        /// 中等权限
        /// </summary>
        Mid = 1,
        /// <summary>
        /// 最高权限
        /// </summary>
        High = 2,
    }
}
