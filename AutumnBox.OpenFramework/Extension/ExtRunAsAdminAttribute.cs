/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:23:33 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 如果添加此标记,则秋之盒会保证该模块以管理员权限运行
    /// </summary>
    public class ExtRunAsAdminAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 设定值
        /// </summary>
        /// <param name="value"></param>
        public ExtRunAsAdminAttribute(bool value) : base(value)
        {
        }
        /// <summary>
        /// 使用默认值true
        /// </summary>
        public ExtRunAsAdminAttribute() : base(true)
        {
        }
    }
}
