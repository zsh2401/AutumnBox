/* =============================================================================*\
*
* Filename: MiFlasherArgs.cs
* Description: 
*
* Version: 1.0
* Created: 10/3/2017 00:36:34(UTC+8:00)
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
    public class MiFlasherArgs: ModuleArgs
    {
        public MiFlasherArgs(DeviceBasicInfo devInfo) : base(devInfo) { }
        public string batFileName { get; set; }
    }
}
