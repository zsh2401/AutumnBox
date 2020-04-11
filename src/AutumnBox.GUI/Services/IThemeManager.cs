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

namespace AutumnBox.GUI.Services
{
    interface IThemeManager
    {
        ThemeMode ThemeMode { get; set; }
        void Reload();
    }
}
