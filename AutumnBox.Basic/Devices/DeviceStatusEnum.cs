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
        Unknow = -3,
        DEBUGGING_DEVICE  = -2,
        NO_DEVICE = -1,
        RUNNING,
        RECOVERY,
        FASTBOOT,
        SIDELOAD,
    }
}
