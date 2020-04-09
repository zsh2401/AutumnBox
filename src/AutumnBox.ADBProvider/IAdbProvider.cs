/*

* ==============================================================================
*
* Filename: IAdbProvider
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:05:41
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ADBProvider
{
    public interface IAdbProvider
    {
        IAdbManager AdbManager { get; }
        void Load();
    }
}
