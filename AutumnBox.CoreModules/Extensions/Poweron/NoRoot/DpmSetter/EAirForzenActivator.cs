/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:27:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT激活空调狗")]
    [ExtName("Set AirForzen as DPM without root", Lang = "en-us")]
    [ExtIcon("Icons.AirForzen.png")]
    [ExtAppProperty(PKGNAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EAirForzenActivator : DpmSetterExtension
    {
        public const string PKGNAME = "me.yourbay.airfrozen";
        public const string CLASSNAME = ".main.core.mgmt.MDeviceAdminReceiver";
        protected override ComponentName ReceiverName
        {
            get
            {
                return ComponentName
                    .FromSimplifiedClassName(PKGNAME, CLASSNAME);
            }
        }
    }
}
