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
        /// <summary>
        /// 异常
        /// </summary>
        Exception = -1,
        /// <summary>
        /// 成功
        /// </summary>
        Successful = 0,
        /// <summary>
        /// 可能成功
        /// </summary>
        MaybeSuccessful = 1,
        /// <summary>
        /// 可能不成功
        /// </summary>
        MaybeUnsuccessful = 2,
        /// <summary>
        /// 不成功
        /// </summary>
        Unsuccessful = 3,
    }
}
