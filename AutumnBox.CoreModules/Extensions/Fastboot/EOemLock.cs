/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/12 16:01:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("加上BL锁", "en-us:Lock oem")]
    [ExtDesc("觉得解BL后不安全?想养老了?", "en-us:Do you want to relock oem for your device?")]
    [ExtIcon("Icons.lock.png")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ExtText("warn", "This will erase all the data on your device, OK?", "zh-cn:此操作将可能清空你设备上的所有数据,确定吗?")]
    [ExtText("warn2", "Once again, this will erase all the data on your device, OK?", "zh-cn:再问一次,此操作将 可 能 清空你设备上的所有数据,确定吗?")]
    internal class EOemLock : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device,TextAttrManager textManager)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                if (!ui.DoYN(textManager["warn"])) return;
                if (!ui.DoYN(textManager["warn2"])) return;
                CommandExecutor executor = new CommandExecutor();
                executor.OutputReceived += (s, e) => ui.WriteOutput(e.Text);
                var exitCode = executor.Fastboot(device, "oem lock").ExitCode;
                ui.Finish(exitCode);
            }
        }
    }
}
