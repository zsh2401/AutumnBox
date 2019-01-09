/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("__Reboot to system")]
    [ExtDeveloperMode(true)]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [UserAgree(" ERebooterAreUSure")]
    internal class ERebooter : LeafExtensionBase
    {
        public const int SYSTEM = 0;
        public const int FASTBOOT = 1;
        public const int RECOVERY = 2;
        public const int _9008 = 3;
        public const string KEY_REBOOT_OPTION = "reboot_option";

        public void Main(IDevice device, Dictionary<string, object> data)
        {
            switch (data[KEY_REBOOT_OPTION])
            {
                case RECOVERY:
                    device.Reboot2Recovery();
                    break;
                case FASTBOOT:
                    device.Reboot2Fastboot();
                    break;
                case _9008:
                    device.Reboot29008();
                    break;
                default:
                    device.Reboot2System();
                    break;
            }

        }
    }
}
