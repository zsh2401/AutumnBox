/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 8:48:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("刷入REC", "en-us:[ROOT]Flast recovery.img")]
    [ObsoleteImageOperator]
    [ExtRequireRoot]
    [ExtIcon("Icons.flash.png")]
    [ExtRegions("zh-CN","zh-HK","zh-TW","zh-SG")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EFlashRecovery : AutumnBoxExtension
    {
        public override int Main(Dictionary<string, object> args)
        {
            var warnMsg = CoreLib.Current.Languages.Get("EObsoleteAndTryImageHelper");
            Ux.Warn(warnMsg);
            return ERR;
        }
    }
}
