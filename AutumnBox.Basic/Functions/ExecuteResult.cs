/* =============================================================================*\
*
* Filename: ExecuteResult.cs
* Description: 
*
* Version: 1.0
* Created: 9/25/2017 07:20:51(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    public class ExecuteResult
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; internal set; } = string.Empty;
        /// <summary>
        /// 是否正确完成
        /// </summary>
        public bool IsSuccessful { get; internal set; } = true;
        /// <summary>
        /// 具体输出
        /// </summary>
        public OutputData OutputData
        {
            get;
            internal set;
        }

        public ExecuteResult()
        {
        }
        public ExecuteResult(OutputData o)
        {
            this.OutputData = o;
        }
    }
}
