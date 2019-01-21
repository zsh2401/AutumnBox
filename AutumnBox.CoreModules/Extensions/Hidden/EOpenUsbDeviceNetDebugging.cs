using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Util;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtHide]
    [ExtName("Enable net-debugging", "zh-cn:开启设备网络调试")]
    [ExtText("Fail", "Failed", "zh-cn:开启或连接失败!")]
    class EOpenUsbDeviceNetDebugging : LeafExtensionBase
    {
        [LMain]
        private void EntryPoint(ILeafUI ui, IDevice device, TextAttrManager texts)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Show();
                if (device is UsbDevice usbDevice)
                {
                    Task<object> dialogTask = null;
                    ui.ShowDialogById("portInputView");
                    dialogTask.Wait();
                    if (ushort.TryParse(dialogTask.Result?.ToString(), out ushort result))
                    {
                        try
                        {
                            usbDevice.OpenNetDebugging(result, true);
                        }
                        catch (AdbCommandFailedException e)
                        {
                            ui.WriteOutput(e.Message);
                            ui.ShowMessage(texts["Failed"]);
                        }
                    }
                }
                else
                {
                    ui.ShowMessage("ERROR!");
                }
                ui.Shutdown();
            }
        }
    }
}
