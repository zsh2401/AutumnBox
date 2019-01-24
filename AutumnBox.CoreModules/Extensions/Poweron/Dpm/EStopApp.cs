/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 1:30:57 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活小黑屋", "en-us:Set StopApp as DPM")]
    [ExtIcon("Icons.stopapp.png")]
    [ExtPriority(ExtPriority.HIGH + 1)]
    internal class EStopApp : DeviceOwnerSetter
    {
        protected override string ComponentName => "web1n.stopapp/.receiver.AdminReceiver";
        protected override string PackageName => "web1n.stopapp";
    }
}
