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

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("黑阀一键激活-安卓8")]
    [ExtName("Activate brevent by one key-Android O", Lang = "en-us")]
    [ExtDesc("一键激活黑阀,但值得注意的是,这样的激活方式,在重启后将失效")]
    [ExtAppProperty("me.piebridge.brevent", AppLabel = "黑阀", AppLabel_en = "Brevent")]
    [ExtMinAndroidVersion(8, 0, 0)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("Icons.brevent.png")]
    public class EBreventActivator8 : OfficialExtension
    {
        BreventServiceActivator activator;
        protected override int VisualMain()
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
            activator = new BreventServiceActivator();
            activator.Init(args);
            WriteLine(App.GetPublicResouce<string>("ExtensionRunning"));
            var exeResult = activator.Run();
            WriteLine(exeResult.OutputData.ToString());
            if (exeResult.ResultType == Basic.FlowFramework.ResultType.Successful)
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
            try
            {
                activator?.ForceStop();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
