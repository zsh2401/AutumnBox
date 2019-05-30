/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Extensions.Poweron.Dpm;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Clear all accounts", "zh-CN:暴力清空所有账号")]
    [ExtDesc("Use the tech by web1n", "zh-CN:使用web1n提供的黑科技暴力清空账号")]
    [ExtIcon("Icons.nuclear.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtText("warn", "Read it in detail, otherwise the consequences will be at your own risk!!\nThere is only one thing you need to do manually: clear the screen lock and fingerprint lock, do it now, make sure there is no screen lock and fingerprint lock, click agree", "zh-cn:详细阅读，否则后果自负!!!\n你需要手动做的只有一件事：清除屏幕锁和指纹锁，现在立刻去做，确保没有屏幕锁和指纹锁后，请点击同意将开始执行操作")]
    [ExtText("pushing", "pushing DpmPro to device", "zh-cn:推送DPMPRO")]
    [ExtText("extracting", "extracting DpmPro", "zh-cn:提取DPMPRO")]
    [ExtText("removing", "removing accounts", "zh-cn:正在移除账号")]
    [ExtText("reboot", "Reboot device?", "zh-cn:似乎成功了,是否重启设备?")]
    internal class EClearAccounts : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device, IClassTextManager text, ITemporaryFloder tmp, IEmbeddedFileManager emb)
        {
            using (ui)
            {
                ui.Icon = this.GetIconBytes();
                ui.Title = this.GetName();
                ui.Show();
                ui.EAgree(text["warn"]);
                ui.EAgree(text["warn"]);
                ui.EAgree(text["warn"]);
                using (var executor = new CommandExecutor())
                {
                    executor.To(e => ui.WriteOutput(e.Text));
                    var dpm = new DpmPro(executor, emb, tmp, device);
                    ui.WriteLine(text["extracting"]);
                    dpm.Extract();
                    ui.WriteLine(text["pushing"]);
                    dpm.PushToDevice();
                    ui.WriteLine(text["removing"]);
                    int exitCode = dpm.RemoveAccounts();
                    if (exitCode == 0 && ui.DoYN(text["reboot"]))
                    {
                        device.Reboot2System();
                    }
                    ui.Finish(exitCode);
                }
            }
        }
    }
}
