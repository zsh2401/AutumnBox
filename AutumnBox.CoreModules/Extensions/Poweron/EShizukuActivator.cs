/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:30:22 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Activate ShizukuManager", "zh-cn:激活ShizukuManager")]
    [ExtIcon("Icons.ShizukuManager.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtText("launchFirst", "Click OK after ShizukuManager has been launched", "zh-cn:启动Shizuku Manager后点击确认")]
    internal class EShizukuActivator : LeafExtensionBase
    {
        private const string PKG_NAME = "moe.shizuku.privileged.api";
        private const string SH_PATH = "/sdcard/Android/data/moe.shizuku.privileged.api/files/start.sh";
        public void EntryPoint(ILeafUI ui, IDevice device, TextAttrManager text)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                ui.AppPropertyCheck(device, PKG_NAME);
                ui.ShowMessage(text["launchFirst"]);
                CommandExecutor executor = new CommandExecutor();
                var result = executor.AdbShell(device, $"sh {SH_PATH}");
                ui.Finish(result.ExitCode);
            }
        }

    }
}
