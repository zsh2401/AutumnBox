/* =============================================================================*\
*
* Filename: DeviceStatusEnum.cs
* Description: 
*
* Version: 1.0
* Created: 8/18/2017 22:09:36(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 设备状态枚举
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        UNKNOW = -1,
        /// <summary>
        /// 无设备
        /// </summary>
        NO_DEVICE = 0,
        /// <summary>
        /// 开机状态
        /// </summary>
        RUNNING = 1,
        /// <summary>
        /// 处于恢复模式
        /// </summary>
        RECOVERY = 2,
        /// <summary>
        /// 处于Fastboot模式
        /// </summary>
        FASTBOOT = 3,
        /// <summary>
        /// 处于sideload模式
        /// </summary>
        SIDELOAD = 4,
    }
}
