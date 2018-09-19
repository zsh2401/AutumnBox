/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:06:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.Util;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.Sh
{
    [ExtName("黑阀一键激活-安卓8")]
    [ExtName("Activate brevent by one key-Android O", Lang = "en-us")]
    [ExtDesc("一键激活黑阀,但值得注意的是,这样的激活方式,在重启后将失效")]
    [ExtAppProperty("me.piebridge.brevent")]
    [ExtMinAndroidVersion(8, 0, 0)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    internal class EBreventActivator8 : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(TargetDevice).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var choice = Ux.DoChoice( "EBreventActivatorOpenNetDebugging", "EBreventActivatorBtnNo", "EBreventActivatorBtnOpen");
                switch (choice)
                {
                    case ChoiceResult.Cancel:
                        return ERR;
                    case ChoiceResult.Left:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.Right:
                        fixAndroidO = true;
                        break;
                }
            }
            new ActivityManager(TargetDevice).StartActivity("me.piebridge.brevent", "ui.BreventActivity");
            WriteRunning();
            var result = TargetDevice.GetShellCommand($"sh /data/data/me.piebridge.brevent/brevent.sh")
                .To(OutputPrinter)
                .Execute();
            if (fixAndroidO && TargetDevice is UsbDevice usbDevice)
            {
                usbDevice.OpenNetDebugging(5555);
            }
            WriteExitCode(result.ExitCode);
            if (result.ExitCode == (int)LinuxReturnCode.None)
            {
                return OK;
            }
            else
            {
                return ERR;
            }
        }
        protected override bool VisualStop()
        {
            return false;
        }
    }
}
