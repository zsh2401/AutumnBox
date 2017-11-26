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
    public sealed class AirForzenActivator : DeviceOwnerSetter
    {

        protected override string PackageName => "me.yourbay.airfrozen";

        protected override string ClassName => ".main.core.mgmt.MDeviceAdminReceiver";
    }
}
