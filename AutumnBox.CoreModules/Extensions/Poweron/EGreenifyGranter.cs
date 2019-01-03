/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Greenify Aggressive Doze", "zh-CN:绿色守护免ROOT模式")]
    [ExtAppProperty(PKG_NAME)]
    [ExtVersion(2019, 1, 3)]
    [ExtIcon("Icons.Greenify.png")]
    [UserAgree("EGreenifyGranterNotice")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class EGreenifyGranter : OfficialVisualExtension
    {
        private const string PKG_NAME = "com.oasisfeng.greenify";

        private const string GRANT_PRE = "pm grant com.oasisfeng.greenify ";
        private const string WRITE_SECURE_SETTINGS = "android.permission.WRITE_SECURE_SETTINGS";
        private const string DUMP = "android.permission.DUMP";
        private const string READ_LOGS = "android.permission.READ_LOGS";
        private const string GET_APP_OPS_STATS = "android.permission.GET_APP_OPS_STATS";

        private int successed = 0;
        private int error = 0;
        protected override int VisualMain()
        {
            WriteInitInfo();
            IDevice device = DeviceSelectedOnCreating;
            Version androidVersion = new DeviceBuildPropGetter(device).GetAndroidVersion();
            CommandExecutor.Result result;
            using (var executor = new CommandExecutor())
            {
                executor.To(OutputPrinter);
                WriteLine("Accessibility service run-on-demand || Aggressive Doze on Android 7.0+ (non-root)");
                result = executor.AdbShell(device, GRANT_PRE, WRITE_SECURE_SETTINGS);
                Count(result);

                WriteLine("Doze on the Go || Aggressive Doze");
                result = executor.AdbShell(device, GRANT_PRE, DUMP);
                Count(result);

                WriteLine("Wake-up Tracker");
                result = executor.AdbShell(device, GRANT_PRE, READ_LOGS);
                Count(result);

                WriteLine("Background-free enforcement on Android 8+ (non-root)");
                if (androidVersion != null && androidVersion >= new Version("8.0"))
                {
                    result = executor.AdbShell(device, GRANT_PRE, GET_APP_OPS_STATS);
                    Count(result);
                }
                result = executor.Adb(device, "am force-stop", PKG_NAME);
                Count(result);
                WriteLine($"successed: {successed} failed:{error}");
                return OK;
            }
        }
        private void Count(CommandExecutor.Result result)
        {
            if (result.ExitCode == 0)
            {
                successed++;
            }
            else
            {
                error++;
            }
        }
        protected override string GetTipByExitCode(int exitCode)
        {
            if (exitCode != 0) return base.GetTipByExitCode(exitCode);

            if (error == 0)
                return base.GetTipByExitCode(exitCode);
            else
                return Res("EGreenifyGranterMaybeSuccess");
        }
    }
}
