/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/18 23:07:38 (UTC +8:00)
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
    /// Context权限等级
    /// </summary>
    internal enum ContextPermissionLevel
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
