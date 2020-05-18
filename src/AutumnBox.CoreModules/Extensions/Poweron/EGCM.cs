using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtRegions("zh-CN")]
    [ExtName("激活极客内存清理")]
    [ExtIcon("Icons.gcm.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ClassText("isNet", "The target is already net device", "zh-cn:已经是网络设备,无需再次激活")]
    [ClassText("notsupport", "Do not support this type of device", "zh-cn:不支持当前设备")]
    internal class EGCM : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, ICommandExecutor executor)
        {
            using (ui)
            {
                using (executor)
                {
                    executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                    var text = ClassTextReaderCache.Acquire<EGCM>();
                    ui.Show();
                    if (device is NetDevice)
                    {
                        ui.ShowMessage(text["isNet"]);
                        ui.EFinish();
                    }
                    else if (device is UsbDevice usbDevice)
                    {
                        var endPoint = usbDevice.OpenNetDebugging(5555);
                        if (endPoint != null)
                        {
                            executor.Adb($"connect {endPoint}");
                        }
                        ui.Finish();
                    }
                    else
                    {
                        ui.Finish(text["notsupport"]);
                    }
                }
            }
        }
    }
}
