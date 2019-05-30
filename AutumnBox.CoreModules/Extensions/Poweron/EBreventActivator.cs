/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:01:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Util;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活黑阈", "en-us:Activate brevent by one key")]
    [ExtDesc("一键激活黑阈,但值得注意的是,这样的激活方式,在重启后将失效", "Activate the brevent service by one key")]
    [ExtPriority(ExtPriority.LOW)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    [ExtText("firstMsg", "This device seems to be the first time to activate the black domain. Please slide to the right in the mobile phone black domain interface, click the boot and then close this popup window.", "zh-cn:此设备似乎是第一次激活黑域,请在手机黑域界面向右滑,点击启动后再关闭此弹窗")]
    internal class EBreventActivator : LeafExtensionBase
    {
        private const string SH_PATH = "/data/data/me.piebridge.brevent/brevent.sh";
        [LMain]
        public void EntryPoint(ILeafUI ui, IDevice device,IClassTextManager text)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                ui.AppPropertyCheck(device, "me.piebridge.brevent");
                CommandExecutor executor = new CommandExecutor();
                executor.OutputReceived += (s, e) => ui.WriteLine(e.Text);
                if (executor.AdbShell(device, $"cat {SH_PATH}").ExitCode != 0)
                {
                    ui.ShowMessage(text["firstMsg"]);
                    Thread.Sleep(2000);
                }
                var result = executor.AdbShell(device,$"sh {SH_PATH}");
                ui.Finish(result.ExitCode);
            }
        }
    }
}
