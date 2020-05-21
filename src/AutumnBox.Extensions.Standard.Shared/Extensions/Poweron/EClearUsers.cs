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
    [ExtName("Clear all users", "zh-CN:暴力清空所有用户")]
    [ExtDesc("Use the tech by web1n", "zh-CN:使用web1n提供的黑科技暴力清空用户，这将会导致你的应用双开失效，以及其他可能的负面效果")]
    [ExtIcon("Icons.nuclear.png")]
    [ClassText("warn", "Read it in detail, otherwise the consequences will be at your own risk!!\nThere is only one thing you need to do manually: clear the screen lock and fingerprint lock, do it now, make sure there is no screen lock and fingerprint lock, click agree", "zh-cn:详细阅读，否则后果自负!!!\n你需要手动做的只有一件事：清除屏幕锁和指纹锁，现在立刻去做，确保没有屏幕锁和指纹锁后，请点击同意将开始执行操作")]
    [ClassText("pushing", "pushing DpmPro to device", "zh-cn:推送DPMPRO")]
    [ClassText("extracting", "extracting DpmPro", "zh-cn:提取DPMPRO")]
    [ClassText("removing", "removing users", "zh-cn:正在移除用户")]
    [ClassText("reboot", "Reboot device?", "zh-cn:似乎成功了,是否重启设备?")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class EClearUsers : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI _ui, IDevice device, ICommandExecutor _executor, IStorage storage, IEmbeddedFileManager emb)
        {
            using var ui = _ui;
            using var executor = _executor;
            executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);

            ui.Show();
            ui.EAgree(this.RxGetClassText("warn"));
            ui.EAgree(this.RxGetClassText("warn"));
            ui.EAgree(this.RxGetClassText("warn"));

            var dpm = new DpmPro(executor, emb, storage, device);
            ui.WriteLineToDetails(this.RxGetClassText("extracting"));
            dpm.Extract();
            ui.WriteLineToDetails(this.RxGetClassText("pushing"));
            dpm.PushToDevice();
            ui.WriteLineToDetails(this.RxGetClassText("removing"));
            int exitCode = dpm.RemoveUsers();
            if (exitCode == 0 && ui.DoYN(this.RxGetClassText("reboot")))
            {
                device.Reboot2System();
            }
            ui.Finish(exitCode == 0 ? StatusMessages.Success : StatusMessages.Failed);
        }
    }
}
