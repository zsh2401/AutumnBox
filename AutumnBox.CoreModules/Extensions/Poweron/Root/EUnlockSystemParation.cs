/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:33:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Root
{
    [ExtName("[ROOT]解锁系统分区")]
    [ExtName("[ROOT]Unlock system paration", Lang = "en-US")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtRequireRoot]
    public class EUnlockSystemParation : AutumnBoxExtension
    {
        public override int Main()
        {
            Ux.ShowLoadingWindow();
            var unlocker = new SystemPartitionUnlocker();
            unlocker.Init(new FlowArgs()
            {
                DevBasicInfo = TargetDevice
            });
            unlocker.Run();
            Ux.CloseLoadingWindow();
            return OK;
        }
    }
}
