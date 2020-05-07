/*

* ==============================================================================
*
* Filename: ILogger
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 17:53:25
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Logging
{
    public interface ILogger
    {
        void Log(string level, string message);
    }
}
