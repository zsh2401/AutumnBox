/* =============================================================================*\
*
* Filename: Events.cs
* Description: 
*
* Version: 1.0
* Created: 9/26/2017 02:18:39(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Devices
{
    using System;
    public delegate void DevicesChangedHandler(object sender, DevicesChangeEventArgs e);
    public class DevicesChangeEventArgs : EventArgs
    {
        public DevicesList DevicesList { get; }
        public DevicesChangeEventArgs(DevicesList devList)
        {
            DevicesList = devList;
        }
    }
}
