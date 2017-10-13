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
    public struct DeviceAdvanceInfo
    {
        public string ID { get; set; } 
        public string ScreenInfo { get; set; } 
        public int? BatteryLevel { get; set; } 
        public double? MemTotal { get; set; }
        public int? StorageTotal { get; set; } 
        public string FlashMemoryType { get; set; } 
        public string SOCInfo { get; set; }
        public bool IsRoot { get; set; }
    }
}
