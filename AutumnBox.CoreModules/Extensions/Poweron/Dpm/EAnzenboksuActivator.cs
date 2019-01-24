/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活第二空间","en-us:Set Anzenboksu as DPM")]
    [ExtIcon("Icons.Anzenbokusu.png")]
    internal class EAnzenboksuActivator : DeviceOwnerSetter
    {
        protected override string PackageName => "com.hld.anzenbokusu";
        protected override string ComponentName => "com.hld.anzenbokusu/.receiver.DPMReceiver";
    }
}
