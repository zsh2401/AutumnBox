/* =============================================================================*\
*
* Filename: DeviceState
* Description: 
*
* Version: 1.5
* Created: 8/18/2017 22:09:36(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备状态拓展
    /// </summary>
    public static class DeviceStateExtension
    {
        /// <summary>
        /// 将字符串解析为DeviceState
        /// </summary>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public static DeviceState ToDeviceState(this string strStatus)
        {
            switch (strStatus)
            {
                case "device":
                    return DeviceState.Poweron;
                case "recovery":
                    return DeviceState.Recovery;
                case "fastboot":
                    return DeviceState.Fastboot;
                case "sideload":
                    return DeviceState.Sideload;
                case "unauthorized":
                    return DeviceState.Unauthorized;
                case "offline":
                    return DeviceState.Offline;
                default:
                    return DeviceState.Unknown;
            }
        }
    }
    /// <summary>
    /// 设备状态枚举
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// 所有状态,这个状态不可能发生在实机上,仅用于某些无关设备的模块
        /// </summary>
        NoMatter = 2401,

        /// <summary>
        /// 无设备
        /// </summary>
        None = 1 << 0,

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
        /// 未知状态
        /// </summary>
        Unknown = 1 << 6,

        /// <summary>
        /// 未允许ADB调试
        /// </summary>
        Unauthorized = 1 << 7,
    }
}
