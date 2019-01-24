/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:48:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活冰箱", "en-us:Set Icebox as DPM")]
    [ExtPriority(ExtPriority.HIGH)]
    [ExtIcon("Icons.icebox.png")]
    internal class EIceBox : DeviceOwnerSetter
    {
        protected override string ComponentName => "com.catchingnow.icebox/.receiver.DPMReceiver";
        protected override string PackageName => "com.catchingnow.icebox";
    }
}
