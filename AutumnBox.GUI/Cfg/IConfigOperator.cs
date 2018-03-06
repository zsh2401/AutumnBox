/* =============================================================================*\
*
* Filename: IConfigOperator
* Description: 
*
* Version: 1.0
* Created: 2017/10/29 21:36:10 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

using System;

namespace AutumnBox.GUI.Cfg
{
    [Obsolete("已使用Settings代替AutumnBox.GUI.Cfg,此处代码现在仅供参考")]
    internal interface IConfigOperator
    {
        ConfigDataLayout Data { get; }
        void SaveToDisk();
        void ReloadFromDisk();
    }
}
