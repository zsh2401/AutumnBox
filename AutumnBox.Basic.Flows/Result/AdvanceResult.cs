/* =============================================================================*\
*
* Filename: AdvanceResult
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 1:05:07 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.Basic.Flows.Result
{
    /// <summary>
    /// 高级结果
    /// </summary>
    public class AdvanceResult : FlowResult
    {
        /// <summary>
        /// 根据返回码判断是否成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return ExitCode == 0;
            }
        }
        /// <summary>
        /// 返回码
        /// </summary>
        public int ExitCode { get; set; } = 0;
    }
}
