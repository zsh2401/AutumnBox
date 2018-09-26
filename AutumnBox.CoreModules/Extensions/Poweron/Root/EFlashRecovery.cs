/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:48:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Root
{
    [ExtName("[ROOT]刷入REC")]
    [ExtName("[ROOT]Flast recovery.img", Lang = "en-US")]
    //[ExtRequireRoot]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EFlashRecovery : AutumnBoxExtension
    {
        public override int Main()
        {
            var warnMsg = CoreLib.Current.Languages.Get("EFlashRecoveryObsoleteMsg");
            Ux.Warn(warnMsg);
            return ERR;
        }
    }
}
