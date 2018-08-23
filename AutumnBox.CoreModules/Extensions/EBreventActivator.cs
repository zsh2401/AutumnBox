/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:01:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("黑阀一键激活")]
    [ExtAuth("zsh2401")]
    [ExtDesc("一键激活黑阀,但值得注意的是,这样的激活方式,在重启后将失效")]
    [ExtAppProperty("me.piebridge.brevent", AppLabel = "黑阀", AppLabel_en = "Brevent")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    [ExtUxEnable(true)]
    public class EBreventActivator : AutumnBoxExtension
    {
        public override int Main()
        {
            var args = new ShScriptExecuterArgs() { DevBasicInfo = TargetDevice, FixAndroidOAdb = false };
            /*开始操作*/
            BreventServiceActivator activator = new BreventServiceActivator();
            activator.Init(args);
            WriteLine("正在操作...");
            activator.Run();
            WriteLine("OK");
            return OK;
        }
    }
}
