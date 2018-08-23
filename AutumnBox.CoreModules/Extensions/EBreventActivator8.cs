/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:06:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("黑阀一键激活-安卓8")]
    [ExtAuth("zsh2401")]
    [ExtDesc("一键激活黑阀,但值得注意的是,这样的激活方式,在重启后将失效")]
    [ExtAppProperty("me.piebridge.brevent")]
    [ExtMinAndroidVersion(8, 0, 0)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    public class EBreventActivator8 : AutumnBoxExtension
    {
        public override int Main()
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(TargetDevice.Serial).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var result = Ux.DoChoice("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.Cancel:
                        return ERR;
                    case ChoiceResult.Deny:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.Accept:
                        fixAndroidO = true;
                        break;
                }
            }
            var args = new ShScriptExecuterArgs() { DevBasicInfo = TargetDevice, FixAndroidOAdb = fixAndroidO };
            /*开始操作*/
            BreventServiceActivator activator = new BreventServiceActivator();
            activator.Init(args);
            WriteLine("正在运行");
            var output = activator.Run();
            WriteLine("OK");
            Logger.Info(output.OutputData.ToString());
            return OK;
        }
    }
}
