using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using MaterialDesignThemes.Wpf;
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
            var leaf = ui.ToLeafUIHideApi();
            var atmbgui = HideApiManager.guiHideApi;
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                if (device is NetDevice netDevice)
                {
                    Task<object> dialogTask = null;
                    ui.RunOnUIThread(() =>
                    {
                        var view = atmbgui.GetNewView("disconnectChoiceView");
                        dialogTask = leaf.GetDialogHost().ShowDialog(view);
                    });
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
