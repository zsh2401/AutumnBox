/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:01:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Util;
using AutumnBox.OpenFramework.Extension;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.Sh
{
    [ExtName("黑阀一键激活")]
    [ExtName("Activate brevent by one key", Lang = "en-us")]
    [ExtDesc("一键激活黑阀,但值得注意的是,这样的激活方式,在重启后将失效")]
    [ExtAppProperty("me.piebridge.brevent")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    internal class EBreventActivator : OfficialVisualExtension
    {
        private const string SH_PATH = "/data/data/me.piebridge.brevent/brevent.sh";
        private const int stateCheck = 0;
        private const int stateExecutingShell = 1;
        private bool requiredStop = false;
        private int state = 0;
        private IProcessBasedCommand executingCommand;
        protected override int VisualMain()
        {
            WriteInitInfo();
            new ActivityManager(TargetDevice).StartActivity("me.piebridge.brevent", "ui.BreventActivity");
            var catCommand = new ShellCommand(TargetDevice, $"cat {SH_PATH}");
            state = stateCheck;
            while (catCommand.Execute().ExitCode != (int)LinuxReturnCode.None && !requiredStop)
            {
                Ux.ShowMessageDialog(Res("EBreventActivatorFirstMsg"));
                Thread.Sleep(2000);
            }
            if (requiredStop)
            {
                return ERR_CANCLLED_BY_USER;
            }
            state = stateExecutingShell;
            executingCommand = TargetDevice.GetShellCommand($"sh {SH_PATH}")
                .To(OutputPrinter);
            var result = executingCommand.Execute();
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
            if (state == stateCheck)
            {
                requiredStop = true;
                return true;
            }
            else
            {
                executingCommand.Kill();
                return true;
            }
        }
    }
}
