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

using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.Device
{
    public struct DeviceHardwareInfo
    {
        public Serial Serial { get; set; }
        public string ScreenInfo { get; set; }
        public int? BatteryLevel { get; set; }
        public double? SizeofRam { get; set; }
        public double? SizeofRom { get; set; }
        public string FlashMemoryType { get; set; }
        public string SOCInfo { get; set; }
    }
}
