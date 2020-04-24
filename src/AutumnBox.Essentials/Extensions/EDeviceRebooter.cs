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
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.Essentials.Extensions
{
    [ExtName("重启设备到恢复模式")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class EDeviceRebooter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI leafUI, IDevice device)
        {
            using (leafUI)
            {
                leafUI.Show();
                if (leafUI.DoYN("重启?", "是", "否"))
                {
                    device.Reboot2Recovery();
                }
                leafUI.Finish();
            }
        }
    }
}
