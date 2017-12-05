/* =============================================================================*\
*
* Filename: FlowResult
* Description: 
*
* Version: 1.1
* Created: 2017/11/23 15:19:26 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using System;

namespace AutumnBox.Basic.FlowFramework
{
    public class FlowResult
    {
        public CheckResult CheckResult { get; set; } = CheckResult.Error;
        public OutputData OutputData { get; set; } = new OutputData();
        public ResultType ResultType { get; set; } = ResultType.Successful;
        public Exception Exception { get; set; }
    }
}
