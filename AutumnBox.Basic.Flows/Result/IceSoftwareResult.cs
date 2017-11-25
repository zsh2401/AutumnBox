/* =============================================================================*\
*
* Filename: IceSoftwareResult
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:24:08 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.Flows.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows.Result
{
    public class IceSoftwareResult:AdvanceResult
    {
        public IceActivatorErrType ErrorType { get; set; } = IceActivatorErrType.None;
    }
}
