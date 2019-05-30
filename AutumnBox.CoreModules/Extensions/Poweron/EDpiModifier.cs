/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:26:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("修改DPI", "en-us:Modify dpi without root")]
    [ExtIcon("Icons.dpi.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtText("msg", "", "zh-cn:")]
    [ExtText("left", "", "zh-cn:")]
    [ExtText("right", "", "zh-cn:")]
    [ExtText("hint", "", "zh-cn:")]
    internal class EDpiModifier : LeafExtensionBase
    {
        [LMain]
        public void Main(IDevice device, ILeafUI ui, IClassTextManager text)
        {
            string messageOfChoice = text["msg"];
            string leftOfChoice = text["left"];
            string rightOfChoice = text["right"];
            string messageInputNumber = text["hint"];
            using (ui)
            {
                ui.Icon = this.GetIconBytes();
                ui.Title = this.GetName();
                ui.Show();
                var choiceResult = ui.DoChoice(messageOfChoice, rightOfChoice, leftOfChoice);
                var wm = new WindowManager(device);
                switch (choiceResult)
                {
                    case null:
                        ui.EShutdown();
                        break;
                    case true:
                        if (int.TryParse(ui.InputString(messageInputNumber), out int target))
                        {
                            wm.Density = target;
                            device.Reboot2System();
                            ui.EFinish();
                        }
                        else
                        {
                            ui.Finish(LeafConstants.ERR_CANCELED_BY_USER);
                        }

                        break;
                    case false:
                        wm.ResetDensity();
                        device.Reboot2System();
                        ui.EFinish();
                        break;
                }
            }
        }
    }
}
