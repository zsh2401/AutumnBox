/* =============================================================================*\
*
* Filename: DeviceHardWareInfo
* Description: 
*
* Version: 2.0
* Created: 2017/10/8 4:51:56(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Device.Management.Hardware
{
    /// <summary>
    /// 设备硬件信息
    /// </summary>
    public struct DeviceHardwareInfo
    {
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// 屏幕信息
        /// </summary>
        public string ScreenInfo { get; set; }
        /// <summary>
        /// 电量信息
        /// </summary>
        public int? BatteryLevel { get; set; }
        /// <summary>
        /// Ram大小
        /// </summary>
        public double? SizeofRam { get; set; }
        /// <summary>
        /// Rom大小
        /// </summary>
        public double? SizeofRom { get; set; }
        /// <summary>
        /// 闪存信息
        /// </summary>
        public string FlashMemoryType { get; set; }
        /// <summary>
        /// Soc信息
        /// </summary>
        public string SOCInfo { get; set; }
    }
}
