/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:42:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("刷入BOOT", "en-us:Flast boot.img")]
    [ExtRegions("zh-CN", "zh-HK", "zh-TW", "zh-SG")]
    [ObsoleteImageOperator]
    [ExtRequireRoot]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EFlashBoot : AutumnBoxExtension
    {
        protected override int Main()
        {
            var warnMsg = CoreLib.Current.Languages.Get("EObsoleteAndTryImageHelper");
            Ux.Warn(warnMsg);
            return ERR;
        }
    }
}
