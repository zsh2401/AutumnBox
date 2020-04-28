/*

* ==============================================================================
*
* Filename: EDeviceRebooter
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 15:26:21
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.Essentials.Extensions
{
    [ExtName("Reboot device", "zh-cn:重启设备")]
    [ExtRequiredDeviceStates(DeviceState.Poweron | DeviceState.Fastboot | DeviceState.Recovery)]
    [ClassText("option_title", "Reboot to?", "重启到")]
    [ClassText("option_tosys", "System", "zh-cn:系统")]
    [ClassText("option_torec", "Recovery mode", "zh-cn:恢复模式")]
    [ClassText("option_tofb", "Fastboot mode", "zh-cn:Fastboot")]
    [ExtIcon("Resources.Icons.reboot.png")]
    public class EDeviceRebooter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI leafUI, IDevice device)
        {

            using (leafUI)
            {
                leafUI.Show();
                var options = new object[] {
                    this.RxGetClassText("option_tosys"),
                    this.RxGetClassText("option_torec"),
                    this.RxGetClassText("option_tofb")
                };
                object result = leafUI.SelectFrom(this.RxGetClassText("option_title"), options);
                if (result == options[0])
                {
                    device.Reboot2System();
                }
                else if (result == options[1])
                {
                    device.Reboot2Recovery();
                }
                else if (result == options[2])
                {
                    device.Reboot2Fastboot();
                }
                leafUI.Finish();
            }
        }
    }
}
