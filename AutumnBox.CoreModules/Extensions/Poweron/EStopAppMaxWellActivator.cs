using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活小黑屋麦克斯韦妖模式", "en-us:StopApp MaxWell")]
    [ExtAppProperty(PKG_NAME)]
    [ExtPriority(ExtPriority.HIGH - 1)]
    [ExtIcon("Icons.stopapp.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    class EStopAppMaxWellActivator : OfficialVisualExtension
    {
        private const string PKG_NAME = "web1n.stopapp";
        private const string MAIN_ACTIVITY = "web1n.stopapp/.activity.MainActivity";
        private const string ACTIVATE_BY_SH = "sh /sdcard/Android/data/web1n.stopapp/files/demon.sh";
        private const string ACTIVATE_COMMAND =
            "CLASSPATH=web1n.stopapp*/base.apk app_process /system/bin com.web1n.stopapp.app_process.DemonStart";

        protected override int VisualMain()
        {
            //启动小黑屋确保sh被释放
            var am = new ActivityManager(DeviceSelectedOnCreating) { CmdStation = this.CmdStation };
            am.To(OutputPrinter);
            try
            {
                am.StartActivity(MAIN_ACTIVITY);
            }
            catch { }
            using (CommandExecutor executor = new CommandExecutor())
            {
                executor.To(OutputPrinter);
                var result = executor.AdbShell(DeviceSelectedOnCreating, ACTIVATE_BY_SH);
                return result.ExitCode;
            }
        }
    }
}
