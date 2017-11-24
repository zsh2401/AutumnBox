/* =============================================================================*\
*
* Filename: Result
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:19:26 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.States;

namespace AutumnBox.Basic.FlowFramework.Container
{
    public class FlowResult
    {
        public CheckResult CheckResult { get; set; }
        public OutputData Output { get; set; } = new OutputData();
        public ResultType ResultType { get; set; } = ResultType.Successful;
    }
}
