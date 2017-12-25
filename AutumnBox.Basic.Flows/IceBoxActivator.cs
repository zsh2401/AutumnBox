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
    public sealed class IceBoxActivator : DeviceOwnerSetter
    {
        //private static readonly
        //    string _defaultCommand = "dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver";
        public static readonly string AppPackageName = "com.catchingnow.icebox";
        protected override string PackageName => AppPackageName;

        protected override string ClassName => ".receiver.DPMReceiver";
    }
}
