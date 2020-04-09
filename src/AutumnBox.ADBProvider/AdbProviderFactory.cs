/*

* ==============================================================================
*
* Filename: AdbProviderFactory
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:08:52
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.ADBProvider.Support.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ADBProvider
{
    public static class AdbProviderFactory
    {
        private const bool IS_WIN32 = true;
        public static IAdbProvider Get(bool autoLoad = false)
        {
            IAdbProvider result;
            if (IS_WIN32)
            {
                result = new Win32AdbProvider();
            }
            if (autoLoad)
            {
                result.Load();
            }
            return result;
        }
    }
}
