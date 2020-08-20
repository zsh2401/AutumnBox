/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/12 16:01:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.CoreModules.Extensions.Fastboot
{
    [ExtName("加上BL锁", "en-us:Lock oem")]
    [ExtDesc("觉得解BL后不安全?想养老了?", "en-us:Do you want to relock oem for your device?")]
    [ExtIcon("Icons.lock.png")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(DeviceState.Fastboot)]
    [ClassText("warn", "This will erase all the data on your device, OK?", "zh-cn:此操作将可能清空你设备上的所有数据,确定吗?")]
    [ClassText("warn2", "Once again, this will erase all the data on your device, OK?", "zh-cn:再问一次,此操作将 可 能 清空你设备上的所有数据,确定吗?")]
    internal class EOemLock : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, ICommandExecutor executor)
        {
            using (ui)
            {
                using (executor)
                {
                    var text = ClassTextReaderCache.Acquire<EOemLock>();
                    ui.Show();
                    if (!ui.DoYN(text["warn"])) return;
                    if (!ui.DoYN(text["warn2"])) return;
                    executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                    var exitCode = executor.Fastboot(device, "oem lock").ExitCode;
                    ui.Finish(exitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
                }
            }
        }
    }
}
