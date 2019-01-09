/*************************************************
** auth： zsh2401@163.com
** date:  2018/11/12 14:57:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("Remove device owner", "zh-cn:清除设备管理员")]
    [ExtDesc("If you want to replace the DPM software, or if you perform the wrong operation during uninstallation, resulting in \"Device Owner Remains\", then use this feature to save your device!", "zh-cn:如果你想要更换DPM软件了，或者是卸载时进行了错误的操作，导致了“Device Owner残留”，那么快用这个功能拯救你的设备吧！")]
    [ExtRequireRoot]
    [UserAgree("本模块将玉石俱焚,并且效果不一定完美，你真的需要这么做吗？如果你只是想要移除某个冻结软件,请前往该软件设置进行卸载")]
    [ExtIcon("Icons.nuclear.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron | DeviceState.Recovery)]
    internal class EDeviceOwnerRemover : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            IProcessBasedCommandResult step1Result = null, step2Result = null;
            step1Result = CmdStation.GetSuCommand(TargetDevice, "rm -rf /data/system/device_policies.xml")
                 .To(OutputPrinter)
                 .Execute();
            WriteExitCode(step1Result.ExitCode);
            step2Result = CmdStation.GetSuCommand(TargetDevice, "rm -rf /data/system/device_owner_2.xml")
                .To(OutputPrinter)
                .Execute();
            WriteExitCode(step2Result.ExitCode);

            if (step1Result.ExitCode == 0 && step2Result.ExitCode == 0)
            {
                bool rebootToSystem = Ux.DoYN(Res("EDeviceOwnerRemoverYNReboot"));
                if (rebootToSystem)
                {
                    TargetDevice.Reboot2System();
                }
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
