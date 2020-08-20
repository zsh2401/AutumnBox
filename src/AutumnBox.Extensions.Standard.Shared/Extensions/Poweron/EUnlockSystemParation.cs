/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:33:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("解锁系统分区", "en-us:[ROOT]Unlock system paration")]
    [ExtDesc("This extension can not unlock BL!", "zh-cn:不是解锁BL！！！这个功能只是为了提供完整的root权限！")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtRequireRoot(true)]
    [ExtIcon("Icons.unlock.png")]
    [ExtAuth("zsh2401")]
    [ClassText("reboot", "Reboot device?", "zh-cn:似乎成功了.\n重启设备生效,是否重启?")]
    [ClassText("warn", "This function is not making your device's boot loader partion unlocked.", "zh-cn:这个模块不是用于解锁手机BL的！")]
    internal class EUnlockSystemParation : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, ICommandExecutor executor)
        {
            using (ui)
            {
                using (executor)
                {
                    var text = ClassTextReaderCache.Acquire<EUnlockSystemParation>();
                    ui.Show();
                    ui.EAgree(text.RxGet("warn"));
                    executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                    executor.Adb(device, "root");
                    var result = executor.Adb(device, "disable-verity");
                    if (result.ExitCode == 0 && ui.DoYN(text["reboot"]))
                    {
                        device.Reboot2System();
                    }
                    ui.Finish(result.ExitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
                }
            }
        }
    }
}
