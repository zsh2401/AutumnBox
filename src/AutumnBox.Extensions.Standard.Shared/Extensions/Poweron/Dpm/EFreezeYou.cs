/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 18:27:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活FreezeYou", "en-us:Set FreezeYou! as DPM")]
    [ExtIcon("Icons.freezeYou.png")]
    internal class EFreezeYou : DeviceOwnerSetter
    {
        protected override string PackageName => "cf.playhi.freezeyou";
        protected override string ComponentName => "cf.playhi.freezeyou/.DeviceAdminReceiver";
    }
}
