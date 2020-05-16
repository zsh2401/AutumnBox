/*

* ==============================================================================
*
* Filename: ModuleInfo
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 20:17:54
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx
{
    /// <summary>
    /// 指示该模块的信息
    /// </summary>
    public static class ModuleInfo
    {
        /// <summary>
        /// 指示版本
        /// </summary>
        public static Version Version => Version.Parse(VERSION_STR);
        const string VERSION_STR = "2020.5.16";
    }
}
