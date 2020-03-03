/*

* ==============================================================================
*
* Filename: IADBManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 16:04:44
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// ADB管理器
    /// </summary>
    public interface IADBManager
    {
        Version Version { get; }
        FileInfo AdbExecutable { get; }
        FileInfo FastbootExecutable { get; }
        DirectoryInfo PlatformToolsDirectory { get; }
    }
}
