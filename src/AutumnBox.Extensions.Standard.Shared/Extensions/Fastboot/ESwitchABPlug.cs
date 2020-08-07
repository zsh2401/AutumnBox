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
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.Text;
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
                ui.Show();
                ui.Closing += (s, e) =>
                {
                    executor.Dispose();
                    return true;
                };
                executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);

                //Checking if device support A/B slot.
                CommandResult result = executor.Fastboot(device, "getvar current-slot 2");

                bool? crtSlot = GetCurrentSlot(result.Output.ToString());

                if (crtSlot != null || AskIfContinueWithMayNotSupport(ui))
                {
                    //Ask if continue
                    var targetSlot = (bool)crtSlot ? "b" : "a";
                    if (AskIfContinueToSwitch(ui))
                    {
                        executor.Fastboot(device, "--set-active=" + targetSlot);
                    }
                }
                else
                {
                    ui.Shutdown();
                }
            }
        }
        static readonly Regex slotParseRegex = new Regex(@"^current-slot:\s(?<slot>\w)$");

        /// <summary>
        /// return true if a ,false if b else null;
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        static bool? GetCurrentSlot(string output)
        {
            var match = slotParseRegex.Match(output);
            if (match.Success)
                return match.Result("${slot}") == "a";
            else
                return null;
        }

        static bool AskIfContinueWithMayNotSupport(ILeafUI ui)
        {
            var msg = text.RxGetClassText("not_support");
            var btnContinueForcely = text.RxGetClassText("continue_force");
            return ui.DoChoice(msg, btnContinueForcely) == true;
        }

        static bool AskIfContinueToSwitch(ILeafUI ui)
        {
            return ui.DoYN(text.RxGetClassText("if_continue"));
        }
        [ClassText("not_support", "You device is not support A/B slot.", "zh-cn:你的设备不支持A/B槽位切换")]
        [ClassText("continue_force", "Continue forcely.", "zh-cn:强行基础")]
        [ClassText("if_continue", "Are you sure to continue? It's may be dangerous.", "zh-cn:确定要继续吗?这可能很危险。")]
        private class TextCarrier { }
    }
}
