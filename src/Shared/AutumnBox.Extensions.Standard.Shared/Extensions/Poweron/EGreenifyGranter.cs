/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Greenify Aggressive Doze", "zh-CN:绿色守护免ROOT模式")]
    [ExtVersion(2019, 1, 3)]
    [ExtIcon("Icons.Greenify.png")]
    [ClassText("notice", "This activator activates most of its ROOT-free functions with reference to the green daemon file. Activation may not be successful due to problems with the Android platform.", "zh-cn:本激活器参照绿色守护文档对其大部分免ROOT功能进行激活,由于安卓平台的问题,激活不一定可以成功")]
    [ClassText("tip", "MaybeSuccess", "zh-cn:可能成功")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class EGreenifyGranter : LeafExtensionBase
    {
        private const string PKG_NAME = "com.oasisfeng.greenify";

        private const string GRANT_PRE = "pm grant com.oasisfeng.greenify ";
        private const string WRITE_SECURE_SETTINGS = "android.permission.WRITE_SECURE_SETTINGS";
        private const string DUMP = "android.permission.DUMP";
        private const string READ_LOGS = "android.permission.READ_LOGS";
        private const string GET_APP_OPS_STATS = "android.permission.GET_APP_OPS_STATS";

        private int successed = 0;
        private int error = 0;
        [LMain]
        public void EntryPoint(ILeafUI _ui, IDevice device, ICommandExecutor _executor)
        {
            using var ui = _ui;
            using var executor = _executor;
            var text = ClassTextReaderCache.Acquire(this.GetType());
            ui.Show();
            ui.EAgree(text["notice"]);
            Version androidVersion = new DeviceBuildPropGetter(device).GetAndroidVersion();
            executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
            ui.WriteLineToDetails("Accessibility service run-on-demand || Aggressive Doze on Android 7.0+ (non-root)");
            var result = executor.AdbShell(device, GRANT_PRE, WRITE_SECURE_SETTINGS);
            Count(result);

            ui.WriteLineToDetails("Doze on the Go || Aggressive Doze");
            result = executor.AdbShell(device, GRANT_PRE, DUMP);
            Count(result);

            ui.WriteLineToDetails("Wake-up Tracker");
            result = executor.AdbShell(device, GRANT_PRE, READ_LOGS);
            Count(result);

            ui.WriteLineToDetails("Background-free enforcement on Android 8+ (non-root)");
            if (androidVersion != null && androidVersion >= new Version("8.0"))
            {
                result = executor.AdbShell(device, GRANT_PRE, GET_APP_OPS_STATS);
                Count(result);
            }
            result = executor.AdbShell(device, "am force-stop", PKG_NAME);
            Count(result);
            ui.WriteLineToDetails($"successed: {successed} failed:{error}");
            ui.Finish(text["tip"]);

        }
        private void Count(CommandResult result)
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
    }
}
