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
    public sealed class IslandActivator : DeviceOwnerSetter
    {
        //like this : "shell dpm set-device-owner com.oasisfeng.island/.IslandDeviceAdminReceiver";
        protected override string PackageName => "com.oasisfeng.island";

        protected override string ClassName => ".IslandDeviceAdminReceiver";
    }
}
