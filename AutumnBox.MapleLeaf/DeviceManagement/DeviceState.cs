/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 18:54:33 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.DeviceManagement
{
    public enum DeviceState : int
    {
        /// <summary>
        /// 开机状态
        /// </summary>
        Poweron = 1 << 1,

        /// <summary>
        /// 处于恢复模式
        /// </summary>
        Recovery = 1 << 2,

        /// <summary>
        /// 处于Fastboot模式
        /// </summary>
        Fastboot = 1 << 3,

        /// <summary>
        /// 处于sideload模式
        /// </summary>
        Sideload = 1 << 4,

        /// <summary>
        /// 处于offline
        /// </summary>
        Offline = 1 << 5,

        /// <summary>
        /// 未允许ADB调试
        /// </summary>
        Unauthorized = 1 << 6,

        /// <summary>
        /// 未知状态
        /// </summary>
        Unknown = 1 << 7,
    }
}
