using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using MaterialDesignThemes.Wpf;
using System.Net;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Connect to net device", "zh-cn:网络设备连接")]
    [ExtHide]
    [ExtText("Fail", "Can not connect to this device", "zh-cn:连接失败")]
    [ExtText("ConnectingDevice", "Connecting to the device", "zh-cn:正在尝试连接这个设备...")]
    [ExtText("PleaseInputRightIP", "Please input a right ip address and port", "zh-cn:请输入正确的IP和端口号")]
    class ENetDeviceConnecter : LeafExtensionBase
    {
        [LMain]
        private void EntryPoint(ILeafUI ui, TextAttrManager texts)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Show();
                if (TryGetInputEndPoint(ui, texts, out IPEndPoint endpoint))
                {
                    using (var executor = new CommandExecutor())
                    {
                        ui.Tip = texts["ConnectingDevice"];
                        ui.WriteLine(texts["ConnectingDevice"]);
                        executor.To(e => ui.WriteOutput(e.Text));
                        var result = executor.Adb($"connect {endpoint.Address}:{endpoint.Port}");
                        if (result.Output.Contains("fail", true))
                        {
                            ui.WriteLine(texts["fail"]);
                            ui.Finish(texts["Fail"]);
                        }
                        else
                        {
                            ui.Finish(result.ExitCode);
                        }
                    }
                }
                else
                {
                    ui.Shutdown();
                }
            }
        }

        private bool TryGetInputEndPoint(ILeafUI ui, TextAttrManager texts, out IPEndPoint endPoint)
        {
            var guiHideApi = HideApiManager.guiHideApi;
            var leafUIHideApi = ui.ToLeafUIHideApi();

            Task<object> dialogTask = null;
            do
            {
                if (dialogTask?.Result is bool)
                {
                    ui.ShowMessage(texts["PleaseInputRightIP"]);
                }
                ui.RunOnUIThread(() =>
                {
                    object view = guiHideApi.GetNewView("inputIpEndPoint");
                    dialogTask = leafUIHideApi.GetDialogHost().ShowDialog(view);
                });
                dialogTask.Wait();
            } while (dialogTask.Result is bool);
            if (dialogTask.Result is IPEndPoint endPointResult)
            {
                endPoint = endPointResult;
                return true;
            }
            else if (dialogTask.Result is string str && str == "iloveyou")
            {
                //我喜欢你,曹娜(*^▽^*)
                System.Diagnostics.Process.Start("https://lovecaona.cn");
            }
            endPoint = null;
            return false;
        }
    }
}
