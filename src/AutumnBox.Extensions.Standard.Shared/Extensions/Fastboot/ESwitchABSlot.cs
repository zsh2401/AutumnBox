/*

* ==============================================================================
*
* Filename: ESwitchABPlug
* Description: 
*
* Version: 1.0
* Created: 2020/8/8 3:18:02
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Text.RegularExpressions;

namespace AutumnBox.Extensions.Standard.Extensions.Fastboot
{
    [ExtName("Switch A/B slot", "zh-cn:切换AB槽位")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtIcon("Icons.a_b.png")]
    /// <summary>
    /// https://github.com/zsh2401/AutumnBox/issues/19
    /// </summary>
    class ESwitchABSlot : LeafExtensionBase
    {
        static readonly TextCarrier text = new TextCarrier();

        [LMain]
        public void Run(ILeafUI ui, ICommandExecutor executor, IDevice device)
        {
            using (ui)
            {
                ui.Icon = this.GetIcon();
                ui.Show();
                ui.Closing += (s, e) =>
                {
                    executor.Dispose();
                    return true;
                };
                executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);

                //Checking if device support A/B slot.
                try
                {
                    ui.StatusInfo = text.RxGetClassText("status_getting_crt_slot");
                    string slot = device.GetVar("current-slot");
                    var targetSlot = slot == "a" ? "b" : "a";
                    if (AskIfContinueToSwitch(ui))
                    {
                        ui.StatusInfo = text.RxGetClassText("status_switching");
                        var result = executor.Fastboot(device, "--set-active=" + targetSlot);
                        if (result.ExitCode == 0)
                        {
                            ui.Finish(StatusMessages.Success);
                        }
                        else
                        {
                            ui.Finish(StatusMessages.Failed);
                        }
                    }
                }
                catch (Exception e)
                {
                    DisplayNotSupportMessage(ui);
                    SLogger<ESwitchABSlot>.Info(e);
                    ui.WriteLineToDetails(e.ToString());
                    ui.Finish(StatusMessages.Failed);
                }
            }
        }

        static void DisplayNotSupportMessage(ILeafUI ui)
        {
            ui.ShowMessage(text.RxGetClassText("not_support"));
        }

        static bool AskIfContinueToSwitch(ILeafUI ui)
        {
            return ui.DoYN(text.RxGetClassText("if_continue"));
        }

        [ClassText("status_getting_crt_slot", "Getting current slot", "zh-cn:正在获取当前槽位")]
        [ClassText("status_switching", "Switching", "zh-cn:正在切换")]
        [ClassText("not_support", "You device is not support A/B slot.", "zh-cn:你的设备不支持A/B槽位切换")]
        [ClassText("if_continue", "Are you sure to continue? It's may be dangerous.", "zh-cn:您的设备支持AB槽切换，确定要继续吗？这可能很危险！")]
        private sealed class TextCarrier { }
    }
}
