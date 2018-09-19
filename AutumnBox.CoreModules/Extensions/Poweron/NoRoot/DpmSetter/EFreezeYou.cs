/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:27:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtRegion("zh-CN")]
    [ExtName("免ROOT激活FreezeYou")]
    [ExtName("Set FreezeYou as DPM without root", Lang ="en-us")]
    [ExtIcon("Icons.freezeYou.png")]
    [ExtAppProperty("me.yourbay.airfrozen")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EFreezeYou : DpmSetterExtension
    {
        public override string ReceiverClassName => "cf.playhi.freezeyou";

        public override string DpmAppPackageName => ".DeviceAdminReceiver";
    }
}
