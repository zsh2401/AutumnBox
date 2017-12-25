/* =============================================================================*\
*
* Filename: BreventServiceActivatorErrorType
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 21:26:59 (UTC+8:00)
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

namespace AutumnBox.Basic.Flows.States
{
    public enum BreventServiceActivatorErrorType
    {
        Unknow = -1,
        None = 0,
        Error = 1,
        ShNotFound = 127,
    }
}
