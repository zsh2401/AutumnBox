/* =============================================================================*\
*
* Filename: DeviceAdvanceInfo
* Description: 
*
* Version: 1.0
* Created: 2017/10/8 4:51:56(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public class DeviceAdvanceInfo
    {
        public string ID { get; set; } = String.Empty;
        public string ScreenInfo { get; set; } = String.Empty;
        public int BatteryLevel { get; set; } = 0;
        public double MemTotal { get; set; } = 0.0;
        public int StorageTotal { get; set; } = 0;
        public string FlashMemoryType { get; set; } = String.Empty;
        public string SOCInfo { get; set; } = String.Empty;
    }
}
