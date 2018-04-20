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
        public virtual string Tag => GetType().Name;
        /// <summary>
        /// 获取权限级别
        /// </summary>
        /// <returns></returns>
        internal virtual ContextPermissionLevel GetPermissionLevel()
        {
            var assName = this.GetType().Assembly.GetName().Name;
            switch (assName)
            {
                case BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME:
                case BuildInfo.AUTUMNBOX_CONSOLE_TESTER_ASSEMBLY_NAME:
                case BuildInfo.AUTUMNBOX_BASIC_ASSEMBLY_NAME:
                    return ContextPermissionLevel.Mid;
                case BuildInfo.AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME:
                    return ContextPermissionLevel.High;
                default:
                    return ContextPermissionLevel.Low;
            }
        }
        /// <summary>
        /// 权限自检
        /// </summary>
        /// <exception cref="AccessDeniedException">如果权限不足,将抛出异常</exception>
        /// <param name="minLevel"></param>
        internal void PermissionCheck(ContextPermissionLevel minLevel = ContextPermissionLevel.Mid)
        {
            if ((int)GetPermissionLevel() < (int)minLevel)
            {
                throw new AccessDeniedException();
            }
        }
    }
}
