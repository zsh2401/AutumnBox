/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:27:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活空调狗", "en-us:Set AirForzen as DPM without root")]
    [ExtIcon("Icons.AirForzen.png")]
    [ExtAppProperty(PKGNAME)]
    [DpmReceiver(RECEIVER_NAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EAirForzenActivator : EDpmSetterBase
    {
        private const string PKGNAME = "me.yourbay.airfrozen";
        private const string RECEIVER_NAME = "me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver";
    }
}
