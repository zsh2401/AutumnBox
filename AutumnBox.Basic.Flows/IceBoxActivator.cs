/* =============================================================================*\
*
* Filename: IceBoxActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:27:17 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 冰箱DPM管理员设置器
    /// </summary>
    public sealed class IceBoxActivator : DeviceOwnerSetter
    {
        /// <summary>
        /// 包名
        /// </summary>
        public const string AppPackageName = "com.catchingnow.icebox";
        /// <summary>
        /// 包名
        /// </summary>
        protected override string PackageName => AppPackageName;
        /// <summary>
        /// 接收器名
        /// </summary>
        protected override string ClassName => ".receiver.DPMReceiver";
    }
}
