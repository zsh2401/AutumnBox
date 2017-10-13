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
        UNKNOW = -1,
        NO_DEVICE = 0,
        RUNNING = 1,
        RECOVERY = 2,
        FASTBOOT = 3,
        SIDELOAD = 4,
    }
}
