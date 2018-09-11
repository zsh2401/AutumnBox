/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:27:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT激活空调狗")]
    [ExtName("Set AirForzen as DPM without root",Lang ="en-us")]
    [ExtIcon("Icons.AirForzen.png")]
    [ExtAppProperty("me.yourbay.airfrozen")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EAirForzenActivator : BasedOnDpmSetterExtension
    {
        public override string ReceiverClassName => "me.yourbay.airfrozen";

        public override string DpmAppPackageName => ".main.core.mgmt.MDeviceAdminReceiver";
    }
}
