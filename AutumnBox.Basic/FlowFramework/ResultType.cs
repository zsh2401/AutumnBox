/* =============================================================================*\
*
* Filename: ResultType
* Description: 
*
* Version: 1.2
* Created: 2017/11/24 18:05:28 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// FunctionFlow执行完后的结果类型
    /// </summary>
    public enum ResultType
    {
        Exception = -1,
        Successful = 0,
        MaybeSuccessful = 1,
        MaybeUnsuccessful = 2,
        Unsuccessful = 3,
    }
}
