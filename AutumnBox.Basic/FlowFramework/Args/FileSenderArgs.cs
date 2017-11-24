/* =============================================================================*\
*
* Filename: FileSenderArgs
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 18:12:38 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework.Args
{
    public class FileSenderArgs : FlowArgs
    {
        public string PathFrom { get; set; }
        public string PathTo { get; set; } = "/sdcard/";
        public string FileName => new FileInfo(PathFrom).Name;
    }
}
