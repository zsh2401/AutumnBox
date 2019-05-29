/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:33:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("解锁系统分区", "en-us:[ROOT]Unlock system paration")]
    [ExtDesc("This extension can not unlock BL!", "zh-cn:不是解锁BL！！！这个功能只是为了提供完整的root权限！")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtRequireRoot]
    [ExtIcon("Icons.unlock.png")]
    internal class EUnlockSystemParation : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, TextAttrManager text)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                CommandExecutor executor = new CommandExecutor();
                executor.OutputReceived += (s, e) => ui.WriteLine(e.Text);
                executor.Adb(device, "root");
                var result = executor.Adb(device, "disable-verity");
                ui.Finish(result.ExitCode);
            }
        }
    }
}
