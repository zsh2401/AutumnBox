/*

* ==============================================================================
*
* Filename: ThemeMode
* Description: 
*
* Version: 1.0
* Created: 2020/3/14 15:39:39
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Theme
{
    enum ThemeMode
    {
        Light = 1 << 0,
        Dark = 1 << 1,
        Auto = 1 << 2
    }
}
