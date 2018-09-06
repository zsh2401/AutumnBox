
/* =============================================================================*\
*
* Filename: FlowArgs
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:11:47 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 功能流程的参数,任何功能流程的参数都要直接使用或继承自此类
    /// </summary>
    public class FlowArgs
    {
        /// <summary>
        /// 指定设备的信息
        /// </summary>
        public IDevice DevBasicInfo { get; set; }
    }
}
