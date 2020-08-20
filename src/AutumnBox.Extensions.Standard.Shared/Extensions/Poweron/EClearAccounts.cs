/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Extensions.Poweron.Dpm;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Clear all accounts", "zh-CN:暴力清空所有账号")]
    [ExtDesc("Use the tech by web1n", "zh-CN:使用web1n提供的黑科技暴力清空账号")]
    [ExtIcon("Icons.nuclear.png")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ClassText("warn", "Read it in detail, otherwise the consequences will be at your own risk!!\nThere is only one thing you need to do manually: clear the screen lock and fingerprint lock, do it now, make sure there is no screen lock and fingerprint lock, click agree", "zh-cn:详细阅读，否则后果自负!!!\n你需要手动做的只有一件事：清除屏幕锁和指纹锁，现在立刻去做，确保没有屏幕锁和指纹锁后，请点击同意将开始执行操作")]
    [ClassText("pushing", "pushing DpmPro to device", "zh-cn:推送DPMPRO")]
    [ClassText("extracting", "extracting DpmPro", "zh-cn:提取DPMPRO")]
    [ClassText("removing", "removing accounts", "zh-cn:正在移除账号")]
    [ClassText("reboot", "Reboot device?", "zh-cn:似乎成功了,是否重启设备?")]
    internal class EClearAccounts : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI _ui, IDevice device, ICommandExecutor _executor, IStorage storage, IEmbeddedFileManager emb)
        {
            using var ui = _ui;
            using var executor = _executor;
            var text = ClassTextReaderCache.Acquire<EClearAccounts>();
            ui.Show();
            ui.EAgree(text["warn"]);
            ui.EAgree(text["warn"]);
            ui.EAgree(text["warn"]);
            executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
            var dpm = new DpmPro(executor, emb, storage, device);
            ui.WriteLineToDetails(text["extracting"]);
            dpm.Extract();
            ui.WriteLineToDetails(text["pushing"]);
            dpm.PushToDevice();
            ui.WriteLineToDetails(text["removing"]);
            int exitCode = dpm.RemoveAccounts();
            if (exitCode == 0 && ui.DoYN(text["reboot"]))
            {
                device.Reboot2System();
            }
            ui.Finish(exitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
        }
    }
}
