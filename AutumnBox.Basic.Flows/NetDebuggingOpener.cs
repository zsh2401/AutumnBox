/* =============================================================================*\
*
* Filename: NetDebuggingOpener
* Description: 
*
* Version: 1.0
* Created: 2017/11/30 22:18:15 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Executer;
namespace AutumnBox.Basic.Flows
{
    public class NetDebuggingOpener : FunctionFlow<FlowArgs, FlowResult>
    {
        protected override OutputData MainMethod(ToolKit<FlowArgs> toolKit)
        {
            return toolKit.Ae("tcpip 5555").Output;
        }
    }
}
