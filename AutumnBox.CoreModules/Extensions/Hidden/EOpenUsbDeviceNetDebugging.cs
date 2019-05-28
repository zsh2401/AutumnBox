using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
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
    [ExtHide]
    [ExtName("Enable net-debugging", "zh-cn:开启设备网络调试")]
    [ExtText("Fail", "Failed", "zh-cn:开启或连接失败!")]
    [ExtText("Hint", "Input a port", "zh-cn:输入一个端口!")]
    [ExtText("InputError", "ERROR INPUT,PLEASE CHECK", "zh-cn:输入了不正确的端口,请重新输入!")]
    class EOpenUsbDeviceNetDebugging : LeafExtensionBase
    {
        [LMain]
        private void EntryPoint(ILeafUI ui, IDevice device, TextAttrManager texts)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Show();
                var dev = (UsbDevice)device;
                ushort? port = null;
                do
                {
                    var input = ui.InputString(texts["Hint"], "5555");
                    if (input == null) return;
                    if (ushort.TryParse(input, out ushort _port))
                    {
                        port = _port;
                    }
                    else
                    {
                        ui.EWarn(texts["InputError"]);
                    }
                } while (port == null);
                dev.OpenNetDebugging((ushort)port, true);
            }
        }
    }
}
