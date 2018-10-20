/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:30:22 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.OpenFramework.Extension;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.Sh
{
    [ExtName("Activate ShizukuManager", "zh-cn:激活ShizukuManager")]
    [ExtAppProperty(PKG_NAME)]
    [ExtIcon("Icons.ShizukuManager.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EShizukuActivator : OfficialVisualExtension
    {
        private const int DELAY_AFTER_LAUNCH = 3000;
        private const string PKG_NAME = "moe.shizuku.privileged.api";
        private const string ACTIVITY_CLASS = "MainActivity";
        private const string SH_PATH = "/sdcard/Android/data/moe.shizuku.privileged.api/files/start.sh";
        protected override int VisualMain()
        {
            WriteInitInfo();
            new ActivityManager(TargetDevice).StartActivity("me.piebridge.brevent", "ui.BreventActivity");
            Thread.Sleep(DELAY_AFTER_LAUNCH);
            var result = CmdStation
                .GetShellCommand(TargetDevice,
                $"sh {SH_PATH}")
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();
            WriteExitCode(result.ExitCode);
            return result.ExitCode;
        }
        
    }
}
