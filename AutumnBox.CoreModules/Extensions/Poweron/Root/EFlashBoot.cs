/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:42:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Root
{
    [ExtName("[ROOT]刷入BOOT")]
    [ExtName("[ROOT]Flast boot.img", Lang = "en-US")]
    //[ExtRequireRoot]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EFlashBoot : AutumnBoxExtension
    {
        public override int Main()
        {
            var warnMsg = CoreLib.Current.Languages.Get("EFlashBootObsoleteMsg");
            Ux.Warn(warnMsg);
            return ERR;
        }
    }
}
