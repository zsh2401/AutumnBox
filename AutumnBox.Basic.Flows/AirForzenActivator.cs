/* =============================================================================*\
*
* Filename: AirForzenActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:06:48 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 空调狗激活器
    /// </summary>
    public sealed class AirForzenActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 空调狗包名
        /// </summary>
        public const string AppPackageName = "me.yourbay.airfrozen";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 空调狗接收器类名
        /// </summary>
        protected override string ClassName => ".main.core.mgmt.MDeviceAdminReceiver";
    }
}
