/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:27:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活空调狗", "en-us:Set AirForzen as DPM")]
    [ExtIcon("Icons.AirForzen.png")]
    internal class EAirForzenActivator : DeviceOwnerSetter
    {
        protected override string PackageName => "me.yourbay.airfrozen";
        protected override string ComponentName => "me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver";
    }
}
