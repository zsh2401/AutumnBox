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

using System.IO;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 与ADB相关的一些硬编码值
    /// </summary>
    public static class AdbConstants
    {
     /// <sumryma>
     /// adb应用程序名
     /// </summary>
        public const string ADB_FILENAME = "adb.exe";
        /// <summary>
        /// fastboot应用程序名
        /// </summary>
        public const string FASTBOOT_FILENAME = "fastboot.exe";
        /// <summary>
        /// 工具的相对路径
        /// </summary>
        public const string toolsPath = @"google\platform-tools\";
        /// <summary>
        /// 完整的adb工具相对路径
        /// </summary>
        public static string FullAdbFileName
        {
            get
            {
                return toolsPath + ADB_FILENAME;
            }
        }
        /// <summary>
        /// 完整的fastboot工具相对路径
        /// </summary>
        public static string FullFastbootFileName
        {
            get
            {
                return toolsPath + FASTBOOT_FILENAME;
            }
        }
        
    }
}
