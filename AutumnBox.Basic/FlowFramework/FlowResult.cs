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
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Executer;
using System;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 功能流程的执行结果,所有功能流程的泛型执行结果类都要继承自此类
    /// </summary>
    public class FlowResult
    {
        /// <summary>
        /// 运行前检查的结果
        /// </summary>
        public CheckResult CheckResult { get; set; } = CheckResult.Error;
        /// <summary>
        /// 运行时产生的标准输出,标准错误
        /// </summary>
        public virtual Output OutputData { get; set; } = Output.Empty;
        /// <summary>
        /// 执行的结果
        /// </summary>
        public virtual ResultType ResultType { get; set; } = ResultType.Successful;
        /// <summary>
        /// 导致功能流程执行失败的异常(正常情况下为null)
        /// </summary>
        public virtual Exception Exception { get; set; }
    }
}
