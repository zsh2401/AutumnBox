/*

* ==============================================================================
*
* Filename: ETest
* Description: 
*
* Version: 1.0
* Created: 2020/8/20 18:02:55
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.Extensions.Essentials.Extensions
{

#if !DEBUG
    [ExtHidden()]
#endif
    [ExtName("测试模块")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    class ETest : LeafExtensionBase
    {
        [LMain]
        public void Main(IDevice device, IUx ux, ILeafUI ui)
        {
            using (ui)
            {
                ui.Show();
                foreach (var kv in device.GetAllVar())
                {
                    ui.Println($"{kv.Key}:{kv.Value}");
                }
                ui.Finish();
            }

        }
    }
}
