/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:09:51 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// Context权限等级
    /// </summary>
    public enum CtxPer
    {
        /// <summary>
        /// 低权限，当前版本中无用 
        /// </summary>
        Low = -1,
        /// <summary>
        /// 未知权限,相当于普通权限
        /// </summary>
        None = 1,
        /// <summary>
        /// 普通级别权限,对秋之盒的操作需要询问用户
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 高级权限,对秋之盒的操作无需询问用户
        /// </summary>
        High = 2,
    }
}
