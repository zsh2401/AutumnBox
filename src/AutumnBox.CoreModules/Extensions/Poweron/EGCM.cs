using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtRegions("zh-CN")]
    [ExtName("激活极客内存清理")]
    [ExtAppProperty("com.ifreedomer.fuckmemory")]
    [ExtIcon("Icons.gcm.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtText("isNet", "The target is already net device", "zh-cn:已经是网络设备,无需再次激活")]
    [ExtText("notsupport", "Do not support this type of device", "zh-cn:不支持当前设备")]
    internal class EGCM : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, IClassTextDictionary text)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                if (device is NetDevice)
                {
                    ui.ShowMessage(text["isNet"]);
                    ui.EFinish();
                }
                else if (device is UsbDevice usbDevice)
                {
                    usbDevice.OpenNetDebugging(5555, true);
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
