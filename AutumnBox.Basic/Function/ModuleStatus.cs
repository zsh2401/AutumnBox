/* =============================================================================*\
*
* Filename: ModuleStatus
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 3:12:17 (UTC+8:00)
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

namespace AutumnBox.Basic.Function
{
    public enum ModuleStatus
    {
        Loading = -1,
        WaitingToRun = 0,
        Running = 1,
        Finished = 2,
        ForceStoped = 3,
    }
}
