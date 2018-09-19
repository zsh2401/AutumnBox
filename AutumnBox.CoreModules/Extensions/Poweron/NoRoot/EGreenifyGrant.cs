/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("Greenify Aggressive Doze")]
    [ExtName("绿色守护嗜睡模式",Lang ="zh-CN")]
    [ExtAppProperty(PKG_NAME)]
    [ExtIcon("Icons.Greenify.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    class EGreenifyGrant : OfficialVisualExtension
    {
        private const string PKG_NAME = "com.oasisfeng.greenify";
        protected override int VisualMain()
        {
            WriteInitInfo();
            Version androidVersion = new DeviceBuildPropGetter(TargetDevice).GetAndroidVersion();
            IProcessBasedCommandResult result = null;
            result = new ShellCommand(TargetDevice,
                "pm grant com.oasisfeng.greenify android.permission.WRITE_SECURE_SETTINGS")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);

            result = new ShellCommand(TargetDevice,
                "pm grant com.oasisfeng.greenify android.permission.DUMP")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);

            result = new ShellCommand(TargetDevice,
                "pm grant com.oasisfeng.greenify android.permission.READ_LOGS")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);

            if (androidVersion.Major >= 8)
            {
                result = new ShellCommand(TargetDevice,
               "pm grant com.oasisfeng.greenify android.permission.READ_APP_OPS_STATS")
               .To(OutputPrinter)
               .Execute();
                WriteExitCode(result.ExitCode);
            }

            result = new ShellCommand(TargetDevice,
                "am force-stop com.oasisfeng.greenify")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);
            return 0;
        }
    }
}
