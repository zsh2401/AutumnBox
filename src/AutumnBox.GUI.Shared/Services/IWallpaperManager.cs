/*

* ==============================================================================
*
* Filename: IWallpaperManager
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 23:43:21
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System.Windows.Media;

namespace AutumnBox.GUI.Services
{
    interface IWallpaperManager
    {
        void SetBrush(Brush brush);
        void Reset();
    }
}
