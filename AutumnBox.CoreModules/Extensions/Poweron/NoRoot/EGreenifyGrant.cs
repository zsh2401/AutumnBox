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
    [ExtVersion(2018,9,26)]
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
            result = CmdStation.GetShellCommand(TargetDevice,
                $"pm grant {PKG_NAME} android.permission.WRITE_SECURE_SETTINGS")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);
            ThrowIfCanceled();

            result = CmdStation.GetShellCommand(TargetDevice,
                $"pm grant {PKG_NAME} android.permission.DUMP")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);
            ThrowIfCanceled();

            result = CmdStation.GetShellCommand(TargetDevice,
                $"pm grant {PKG_NAME} android.permission.READ_LOGS")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);
            ThrowIfCanceled();

            if (androidVersion.Major >= 8)
            {
                result = CmdStation.GetShellCommand(TargetDevice,
               $"pm grant {PKG_NAME} android.permission.READ_APP_OPS_STATS")
               .To(OutputPrinter)
               .Execute();
                WriteExitCode(result.ExitCode);
                ThrowIfCanceled();
            }

            result = CmdStation.GetShellCommand(TargetDevice,
                $"am force-stop " + PKG_NAME)
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(result.ExitCode);
            ThrowIfCanceled();
            return 0;
        }
    }
}
