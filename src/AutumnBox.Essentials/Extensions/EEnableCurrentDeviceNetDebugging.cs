/*

* ==============================================================================
*
* Filename: EEnableCurrentDeviceNetDebugging
* Description: 
*
* Version: 1.0
* Created: 2020/5/18 10:35:36
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System.Net;
using System.Threading;

namespace AutumnBox.Essentials.Extensions
{
    [ExtPriority(ExtPriority.HIGH)]
    [ExtName("Enable device net-debugging", "zh-CN:开启当前设备网络调试")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ClassText("input_hint", "Input port", "zh-CN:输入端口号")]
    [ClassText("is_not_usb_device", "Current device is not usb device", "zh-CN:当前设备不是通过USB连接")]
    [ClassText("stauts_enabling", "Enabling device net-debugging", "zh-CN:正在启动设备网络调试")]
    [ClassText("status_connecting", "Connecting to device", "zh-CN:正在尝试连接")]
    [ClassText("can_not_read_ip", "Net-debugging is enabled,but can't read device's ip address. please connect the device manually.", "zh-CN:网络调试开启成功,但无法获取设备IP地址,因此无法进行自动连接")]
    [ClassText("yn_try_to_connect", "It's seems like success.Do you want to try to connect this net device?", "zh-CN:似乎成功了,你想要连接到这个设备吗?")]
    [DeviceNetDebuggingPolicy]
    class EEnableCurrentDeviceNetDebugging : LeafExtensionBase
    {
        class DeviceNetDebuggingPolicy : ExtNormalRunnablePolicyAttribute
        {
            public override bool IsRunnable(RunnableCheckArgs args)
            {
                return base.IsRunnable(args) && args.TargetDevice is UsbDevice;
            }
        }
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, ICommandExecutor executor)
        {
            using (ui)
            {
                ui.Show();
                if (device is UsbDevice usbDevice)
                {
                    string input = null;
                    ushort port = 0;
                    do
                    {
                        input = ui.InputString(this.RxGetClassText("input_hint"), "5555");
                        if (input == null)
                        {
                            ui.EShutdown();
                        }
                    } while (!ushort.TryParse(input, out port));
                    ui.StatusInfo = this.RxGetClassText("stauts_enabling");
                    IPEndPoint endPoint = null;
                    try
                    {
                        endPoint = usbDevice.OpenNetDebugging(port);
                    }
                    catch (AdbCommandFailedException e)
                    {
                        ui.WriteLineToDetails($"exit code : {e.ExitCode}");
                        ui.EShutdown();
                    }

                    ui.WriteLineToDetails($"Device's IPEndPoint: {endPoint?.ToString() ?? "Can not read"}");
                    if (endPoint != null)
                    {
                        if (ui.DoYN(this.RxGetClassText("yn_try_to_connect")))
                        {
                            ui.StatusInfo = this.RxGetClassText("status_connecting");
                            Thread.Sleep(1500);
                            string msg = executor.Adb($"connect {endPoint}").ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed;
                            ui.Finish(msg);
                        }
                        else
                        {
                            ui.Finish(StatusMessages.Success);
                        }
                    }
                    else
                    {
                        ui.ShowMessage(this.RxGetClassText("can_not_read_ip"));
                        ui.Finish(StatusMessages.Success);
                    }
                }
                else
                {
                    ui.ShowMessage(this.RxGetClassText("is_not_usb_device"));
                    ui.Shutdown();
                }
            }
        }
    }
}
