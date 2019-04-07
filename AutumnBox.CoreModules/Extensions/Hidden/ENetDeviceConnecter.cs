using AutumnBox.Basic.Calling;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Util;
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
            Task<object> dialogTask = null;
            do
            {
                if (dialogTask?.Result is bool)
                {
                    ui.ShowMessage(texts["PleaseInputRightIP"]);
                }
                dialogTask = ui.ShowDialogById("inputIpEndPoint");
                dialogTask.Wait();
            } while (dialogTask.Result is bool);
            if (dialogTask.Result is IPEndPoint endPointResult)
            {
                endPoint = endPointResult;
                return true;
            }
            else if (dialogTask.Result is string str)
            {
                switch (str.ToLower())
                {
                    case "iloveyou":
                        ui.ShowMessage("经历了这么多,我才明白最珍贵的其实是你也爱我。\n抱歉曾经对你的冷漠，现在，我只爱你，Yin Yin。");
                        break;
                    case "future":
                        ui.ShowMessage("你要相信我,我们一定可以成功,我们的未来一定是可期的。这不是空口无凭，我一直在努力去让自己变得更好，即便现在的我在最错误的道路上，但总有一天，冰会化成水的，我也会回到正确的方向上");
                        break;
                    case "fear":
                        ui.ShowMessage("猫猫,那天晚上你真的吓到我了...我真的好害怕失去你,这么多年以来,我第一次如此珍视一个人...我是男子汉,我不敢哭...求求你,不要这样对我了,好吗");
                        break;
                    case "dream":
                        ui.ShowMessage("我总是在梦里梦到你,好开心鸭!!!!我一定要在把你紧紧抱住,抱2分钟!!!然后松开,摸摸你的头,然后吻上去!!!");
                        break;
                    case "somuch":
                        ui.ShowMessage("我有太多太多想说的了,可我不知道该怎么对你说,我,爱,你");
                        break;
                }
            }
            endPoint = null;
            return false;
        }
    }
}
