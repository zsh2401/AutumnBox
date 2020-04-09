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
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtText("msg", "What't your want?", "zh-cn:您要做哪一个操作")]
    [ExtText("modify", "Modify DPI", "zh-cn:修改DPI")]
    [ExtText("reset", "Reset DPI", "zh-cn:重设DPI")]
    [ExtText("hint", "Input modify dpi", "zh-cn:请输入要修改的DPI,一定要慎重(建议300-1000)")]
    internal class EDpiModifier : LeafExtensionBase
    {
        [LMain]
        public void Main(IDevice device, ILeafUI ui, IClassTextDictionary text)
        {
            using (ui)
            {
                ui.Icon = this.GetIconBytes();
                ui.Title = this.GetName();
                ui.Show();
                var choiceResult = ui.DoChoice(text["msg"], text["modify"], text["reset"]);
                var wm = new WindowManager(device);
                switch (choiceResult)
                {
                    case null:
                        ui.EShutdown();
                        break;
                    case true:
                        if (int.TryParse(ui.InputString(text["hint"]), out int target))
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
