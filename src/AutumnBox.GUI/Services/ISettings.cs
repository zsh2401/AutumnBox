#nullable enable
/*

* ==============================================================================
*
* Filename: ISettings
* Description: 
*
* Version: 1.0
* Created: 2020/5/20 21:51:23
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AutumnBox.GUI.Services
{
    interface ISettings : INotifyPropertyChanged
    {
        string LanguageCode { get; set; }
        ThemeMode Theme { get; set; }
        bool DeveloperMode { get; set; }
        bool ShowDebugWindowNextLaunch { get; set; }
        bool StartCmdAtDesktop { get; set; }
        bool EnvVarCmdWindow { get; set; }
        bool GuidePassed { get; set; }
        bool SoundEffect { get; set; }
    }
}
