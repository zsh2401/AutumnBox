/*

* ==============================================================================
*
* Filename: IStorageManager
* Description: 
*
* Version: 1.0
* Created: 2020/5/19 19:56:32
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

namespace AutumnBox.GUI.Services
{
    interface IStorageManager
    {
        DirectoryInfo CacheDirectory { get; }
        DirectoryInfo StorageDirectory { get; }
        bool IsFirstLaunch { get; }
    }
}
