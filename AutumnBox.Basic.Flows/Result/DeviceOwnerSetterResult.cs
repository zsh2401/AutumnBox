/* =============================================================================*\
*
* Filename: IceSoftwareResult
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:24:08 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Flows.States;

namespace AutumnBox.Basic.Flows.Result
{
    /// <summary>
    /// 设备管理员设置器完成结果
    /// </summary>
    public class DeviceOwnerSetterResult:AdvanceResult
    {
        /// <summary>
        /// 设置设备管理员时遇到的错误类型
        /// </summary>
        public DeviceOwnerSetterErrType ErrorType { get; set; } = DeviceOwnerSetterErrType.None;
    }
}
