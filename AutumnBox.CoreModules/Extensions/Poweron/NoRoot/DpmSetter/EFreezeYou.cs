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
    [ExtRegion("zh-CN")]
    [ExtName("免ROOT激活FreezeYou", "en-us:Set FreezeYou as DPM without root")]
    [ExtIcon("Icons.freezeYou.png")]
    [ExtAppProperty(PKGNAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EFreezeYou : DpmSetterExtension
    {
        public const string PKGNAME = "cf.playhi.freezeyou";
        public const string CLASSNAME = "DeviceAdminReceiver";
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
