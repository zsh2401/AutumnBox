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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    public interface IConfigOperator
    {
        ConfigTemplate Data { get; }
        void SaveToDisk();
        void ReloadFromDisk();
    }
}
