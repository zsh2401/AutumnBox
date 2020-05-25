/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 20:07:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活黑洞", "en-us:Set Blackhole as DPM")]
    [ExtIcon("Icons.blackhole.png")]
    internal class EBlackHole : DeviceOwnerSetter
    {
        protected override string ComponentName => "com.hld.apurikakusu/.receiver.DPMReceiver";
        protected override string PackageName => "com.hld.apurikakusu";
    }
}
