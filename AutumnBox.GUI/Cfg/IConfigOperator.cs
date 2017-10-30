/* =============================================================================*\
*
* Filename: Class1
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

namespace AutumnBox.GUI.Cfg
{
    internal interface IConfigOperator
    {
        ConfigDataLayout Data { get; }
        void SaveToDisk();
        void ReloadFromDisk();
    }
}
