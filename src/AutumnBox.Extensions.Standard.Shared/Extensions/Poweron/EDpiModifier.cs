/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:26:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Modify dpi without root", "zh-cn:修改DPI")]
    [ExtIcon("Icons.dpi.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ClassText("msg", "Which is your want?", "zh-cn:您要做哪一个操作")]
    [ClassText("modify", "Modify DPI", "zh-cn:修改DPI")]
    [ClassText("reset", "Reset DPI", "zh-cn:重设DPI")]
    [ClassText("hint", "Input modify dpi", "zh-cn:请输入要修改的DPI,一定要慎重(建议300-1000)")]
    internal class EDpiModifier : LeafExtensionBase
    {
        [LMain]
        public void Main(IDevice device, ILeafUI ui, ICommandExecutor executor)
        {
            using (ui)
            {
                using (executor)
                {
                    var text = ClassTextReaderCache.Acquire<EDpiModifier>();
                    ui.Show();
                    var choiceResult = ui.DoChoice(text.RxGet("msg"), text.RxGet("modify"), text.RxGet("reset"));
                    var wm = new WindowManager(device, executor);
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
                                ui.Finish(StatusMessages.CancelledByUser);
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
}
