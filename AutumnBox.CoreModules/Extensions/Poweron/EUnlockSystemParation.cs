/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:33:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("解锁系统分区", "en-us:[ROOT]Unlock system paration")]
    [ExtDesc("不是解锁BL！！！这个功能只是为了提供完整的root权限！")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtRequireRoot]
    [ExtIcon("Icons.unlock.png")]
    internal class EUnlockSystemParation : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            var enableRootResult = CmdStation
                 .GetAdbCommand(TargetDevice,
                 $"root")
                 .To(OutputPrinter)
                 .Execute();
           
            if (enableRootResult.ExitCode != 0)
            {
                return enableRootResult.ExitCode;
            }
            
            Thread.Sleep(2000);
            var result = CmdStation
                .GetAdbCommand(TargetDevice,
                $"disable-verity")
                .To(OutputPrinter)
                .Execute();

           
            return result.ExitCode;
        }
    }
}
