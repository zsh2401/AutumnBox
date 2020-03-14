/*

* ==============================================================================
*
* Filename: IThemeManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/14 15:37:53
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
    interface IThemeManager
    {
        ThemeMode ThemeMode { get; set; }
        void Reload();
    }
}
