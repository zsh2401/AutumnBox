/*

* ==============================================================================
*
* Filename: ENetDeviceDisconnecter
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 21:35:57
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
    [ExtName("Disconnect net-debugging", "zh-CN:断开当前设备")]
    [ExtPriority(ExtPriority.HIGH)]
    [ExtIcon("Resources.Icons.disconnect-netdebbugging.png")]
    [IsNetDevicePolicy]
    [ClassText("option_disconnect", "Only disconnect", "zh-cn:仅断开")]
    [ClassText("option_close", "Disable net debug", "zh-cn:关闭")]
    [ClassText("option_title", "Close net debugging of the device after disconnected?", "zh-cn:断开网络连接后,是否关闭设备的USB网络调试?")]
    class ENetDeviceDisconnecter : LeafExtensionBase
    {
        private class IsNetDevicePolicy : ExtNormalRunnablePolicyAttribute
        {
            public override bool IsRunnable(RunnableCheckArgs args)
            {
                return args.TargetDevice is NetDevice;
            }
        }
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device)
        {
            using (ui)
            {
                ui.Show();
                bool? choice = ui.DoChoice(this.RxGetClassText("option_title"), this.RxGetClassText("option_close"), this.RxGetClassText("option_disconnect"));
                if (choice == null)
                {
                    ui.Shutdown();
                    return;
                }
                else
                {
                    ((NetDevice)device).Disconnect((bool)choice);
                    ui.Finish(StatusMessages.Success);
                }
            }
        }
    }
}
