/* =============================================================================*\
*
* Filename: FunctionArgs.cs
* Description: 
*
* Version: 1.0
* Created: 10/5/2017 04:26:24(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Function.Args
{
    public class ModuleArgs : BaseObject
    {
        public ModuleArgs(DeviceBasicInfo device)
        {
            DeviceBasicInfo = device;
        }
        public DeviceBasicInfo DeviceBasicInfo { get; set; }
    }
}
