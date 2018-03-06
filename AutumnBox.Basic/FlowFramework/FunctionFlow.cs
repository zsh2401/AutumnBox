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
    /// <summary>
    /// 默认参数和默认结果类型的功能流程类
    /// </summary>
    public abstract class FunctionFlow : FunctionFlow<FlowArgs, FlowResult> { }
    /// <summary>
    /// 自定义参数和默认结果类型的功能流程类
    /// </summary>
    /// <typeparam name="TArgs">泛型参数</typeparam>
    public abstract class FunctionFlow<TArgs> : FunctionFlow<TArgs, FlowResult>
        where TArgs : FlowArgs, new()
    { }
}
