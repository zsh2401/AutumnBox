/* =============================================================================*\
*
* Filename: FilePuller
* Description: 
*
* Version: 1.0
* Created: 2017/11/10 18:01:40 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Devices;

namespace AutumnBox.Basic.Function.Args
{
    public class FilePullArgs : ModuleArgs
    {
        public string LocalFilePath { get; set; } = ".";
        public string PhoneFilePath { get; set; } = String.Empty;
        public FilePullArgs(DeviceBasicInfo device) : base(device)
        {
        }
    }
}
