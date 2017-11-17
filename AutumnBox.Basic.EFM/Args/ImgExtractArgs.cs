/* =============================================================================*\
*
* Filename: ImgExtractArgs
* Description: 
*
* Version: 1.0
* Created: 2017/11/10 17:49:02 (UTC+8:00)
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
    public enum Image
    {
        Boot,
        Recovery,
    }
    public class ImgExtractArgs : ModuleArgs
    {
        public string SavePath { get; set; }
        public Images ExtractImage { get; set; }
        public ImgExtractArgs(DeviceBasicInfo device, Images extractImage = Images.Recovery) : base(device)
        {
            ExtractImage = extractImage;
        }
    }
}
