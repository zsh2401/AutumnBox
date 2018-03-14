/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
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
        public virtual string Tag => this.GetType().Name;
        public virtual ContextPermissionLevel GetPermissionLevel()
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
    }
    public enum ContextPermissionLevel
    {
        Low = 0,
        Mid = 1,
        High = 2,
    }
}
