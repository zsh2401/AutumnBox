/* =============================================================================*\
*
* Filename: FunctionFlow
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 21:17:48 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.FlowFramework
{
    public abstract class FunctionFlow : FunctionFlow<FlowArgs, FlowResult>
    {
    }
    public abstract class FunctionFlow<TArgs> : FunctionFlow<TArgs, FlowResult>
        where TArgs : FlowArgs, new()
    {
    }
}
