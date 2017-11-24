/* =============================================================================*\
*
* Filename: LinuxReturnCodes
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 0:56:22 (UTC+8:00)
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
    enum LinuxReturnCodes : int
    {
        NoError = 0,
        Error = 1,
        NoSuchFileOrDirectory = 2,
        KeyHasExpired = 127,
    }
}
