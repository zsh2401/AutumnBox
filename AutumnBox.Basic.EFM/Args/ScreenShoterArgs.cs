/* =============================================================================*\
*
* Filename: ScreenShoterArgs.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 03:54:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.Function.Args
{
    public class ScreenShoterArgs: ModuleArgs
    {
        public ScreenShoterArgs(DeviceBasicInfo device) : base(device) { }
        public string LocalPath { get { return localPath; } set { localPath = value; } }
        string localPath = ".";
    }
}
