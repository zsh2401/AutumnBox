/* =============================================================================*\
*
* Filename: IslandActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/26 19:39:08 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// Island的DPM 设备管理员设置器
    /// </summary>
    public sealed class IslandActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 包名
        /// </summary>
        public const string AppPackageName = "com.oasisfeng.island";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 类名
        /// </summary>
        protected override string ClassName => ".IslandDeviceAdminReceiver";
    }
}
