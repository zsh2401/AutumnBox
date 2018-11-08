/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/23 14:38:18 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Service
{
    /// <summary>
    /// 服务状态
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// 准备
        /// </summary>
        Ready,
        /// <summary>
        /// 运行中
        /// </summary>
        Running,
        /// <summary>
        /// 已停止
        /// </summary>
        Stopped,
    }
}
