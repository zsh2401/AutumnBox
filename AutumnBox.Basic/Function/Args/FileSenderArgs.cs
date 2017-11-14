/* =============================================================================*\
*
* Filename: FileSenderArgs
* Description: 
*
* Version: 1.0
* Created: 2017/11/14 17:06:51 (UTC+8:00)
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
using AutumnBox.Basic.Devices;
using System.IO;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Function.Args
{
    public class FileSenderArgs : ModuleArgs
    {

        public string FilePath { get; set; }
        public FileInfo FileInfo { get { return new FileInfo(FilePath); } }
        public string SavePath { get; set; } = "/sdcard/";
        public string SaveName
        {
            get { return _saveName ?? FileInfo.Name; }
            set { _saveName = value; }
        }
        public string _saveName;
        public FileSenderArgs(DeviceBasicInfo device) : base(device)
        {
        }
    }
}
