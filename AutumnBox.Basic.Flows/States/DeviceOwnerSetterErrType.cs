/* =============================================================================*\
*
* Filename: IceBoxAndAirForzenActivatorErrType
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:23:39 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows.States
{
    /// <summary>
    /// 设备管理员设置器执行结果的错误类型
    /// </summary>
    public enum DeviceOwnerSetterErrType
    {
        /// <summary>
        /// 没有错误
        /// </summary>
        None,
        /// <summary>
        /// 账户没删除干净
        /// </summary>
        ServalAccountsOnTheDevice,
        /// <summary>
        /// 多用户没删除干净||应用双开没删干净||访客模式没关掉
        /// </summary>
        ServalUserOnTheDevice,
        /// <summary>
        /// 这个是啥错误来着忘了,但是可以修复
        /// </summary>
        DeviceAlreadyProvisioned,
        /// <summary>
        /// 已设定设备所有者
        /// </summary>
        DeviceOwnerIsAlreadySet,
        /// <summary>
        /// MIUI下才会出现的问题,开启USB调试的安全设置即可!
        /// </summary>
        MIUIUsbSecError,
        /// <summary>
        /// 压根没装这软件!
        /// </summary>
        UnknowAdmin,
        /*Unknow Error*/
        /// <summary>
        /// 未知问题
        /// </summary>
        Unknow,
        /// <summary>
        /// 未知的java异常
        /// </summary>
        UnknowJavaException,
    }
}
