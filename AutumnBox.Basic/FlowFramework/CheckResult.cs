/* =============================================================================*\
*
* Filename: CheckResult
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 16:17:24 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// FunctionFlow检查函数返回结果
    /// </summary>
    public enum CheckResult
    {
        /// <summary>
        /// 没问题
        /// </summary>
        OK = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 1,
        /// <summary>
        /// 功能流程未完成
        /// </summary>
        Unfinish = 2,
        /// <summary>
        /// 参数错误
        /// </summary>
        ArgError,
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknowError,
        /// <summary>
        /// 设备状态错误
        /// </summary>
        DeviceStateError,
        /// <summary>
        /// 设备序列号错误
        /// </summary>
        DeviceSerialError,
    }
}
