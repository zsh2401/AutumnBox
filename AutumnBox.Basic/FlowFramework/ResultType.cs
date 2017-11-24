/* =============================================================================*\
*
* Filename: ErrorType
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 18:05:28 (UTC+8:00)
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

namespace AutumnBox.Basic.FlowFramework
{
    public enum ResultType
    {
        Successful = 0,
        MaybeSuccessful = 1,
        MaybeUnsuccessful = 2,
        Unsuccessful = 3,
    }
}
