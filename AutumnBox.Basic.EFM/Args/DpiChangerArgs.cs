/* =============================================================================*\
*
* Filename: DpiChangerArgs.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 20:00:30(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Function.Args
{
    public class DpiChangerArgs : ModuleArgs
    {
        public DpiChangerArgs(DeviceBasicInfo device) : base(device) { }
        public int Dpi { get; set; }
    }
}
