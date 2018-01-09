/* =============================================================================*\
*
* Filename: ImgFlasherArgs
* Description: 
*
* Version: 1.0
* Created: 2017/11/14 16:39:09 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.Function.Args
{
    public class ImgFlasherArgs : ModuleArgs
    {
        public string ImgPath { get; set; }
        public Images ImgType { get; set; } = Images.Recovery;
        public ImgFlasherArgs(DeviceBasicInfo device) : base(device)
        {
        }
    }
}
