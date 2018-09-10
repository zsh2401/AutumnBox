/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:01:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Util;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("黑阀一键激活")]
    [ExtName("Activate brevent by one key", Lang = "en-us")]
    [ExtDesc("一键激活黑阀,但值得注意的是,这样的激活方式,在重启后将失效")]
    [ExtAppProperty("me.piebridge.brevent", AppLabel = "黑阀", AppLabel_en = "Brevent")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    public class EBreventActivator : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            new ActivityManager(TargetDevice).StartActivity("me.piebridge.brevent", "ui.BreventActivity");
            WriteLine(App.GetPublicResouce<string>("ExtensionRunning"));
            var result = TargetDevice.GetShellCommand($"sh /data/data/me.piebridge.brevent/brevent.sh")
                .To(OutputPrinter)
                .Execute();
            if (result.ExitCode == (int)LinuxReturnCode.None)
            {
                return OK;
            }
            else
            {
                return ERR;
            }
        }
    }
}
