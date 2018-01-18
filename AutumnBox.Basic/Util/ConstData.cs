/* =============================================================================*\
*
* Filename: ConstData
* Description: 
*
* Version: 1.0
* Created: 2017/10/28 18:43:06 (UTC+8:00)
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

namespace AutumnBox.Basic.Util
{
    public static class ConstData
    {
        public const string AUTUMNBOX_TEMP_FLODER = "/sdcard/autumnbox_temp";
        public const string ADB_FILENAME = "adb.exe";
        public const string FASTBOOT_FILENAME = "fastboot.exe";
        public static string FullAdbFileName {
            get {
                return toolsPath + ADB_FILENAME;
            }
        }
        public static string FullFastbootFileName
        {
            get
            {
                return toolsPath + FASTBOOT_FILENAME;
            }
        }
        public static readonly string toolsPath = @"google\platform-tools\";
    }
}
