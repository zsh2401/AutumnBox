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
    [ExtRegions("zh-CN")]
    [ExtName("免ROOT激活FreezeYou", "en-us:Set FreezeYou as DPM without root")]
    [ExtIcon("Icons.freezeYou.png")]
    [ExtAppProperty(PKGNAME)]
    [DpmReceiver(RECEIVER_NAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EFreezeYou : EDpmSetterBase
    {
        public const string PKGNAME = "cf.playhi.freezeyou";
        private const string RECEIVER_NAME = "cf.playhi.freezeyou/.DeviceAdminReceiver";
    }
}
