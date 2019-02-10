using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Util;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Net device disconnecting", "zh-cn:断开网络设备连接")]
    [ExtHide]
    public class ENetDeviceDisconnecter : LeafExtensionBase
    {
        [LMain]
        private void EntryPoint(ILeafUI ui, IDevice device, TextAttrManager texts)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                if (device is NetDevice netDevice)
                {
                    Task<object> dialogTask = ui.ShowDialogById("disconnectChoiceView");
                    dialogTask.Wait();
                    bool? choice = (bool?)dialogTask.Result;
                    switch (choice)
                    {
                        case null:
                            ui.Shutdown();
                            break;
                        default:
                            netDevice.Disconnect((bool)choice);
                            break;
                    }
                    ui.Shutdown();
                }
                else
                {
                    ui.ShowMessage("THIS DEVICE IS NOT NET DEVICE!");
                    ui.Shutdown();
                }
            }
        }
    }
}
