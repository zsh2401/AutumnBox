/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    [ExtRequiredDeviceStates(NoMatter)]
    //[ExtDesc("wtf")]
    //[ObsoleteImageOperator]
    //[ExtAppProperty("com.miui.calculatorx")]
    [UserAgree("Please be true")]
    internal class EHoldMyHand : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            //WriteLine("Step1");
            //Thread.Sleep(3000);
            //WriteLine("Step2");
            //CstmDpmCommander dpm = new CstmDpmCommander(this, TargetDevice)
            //{
            //    CmdStation = this.CmdStation
            //};
            //dpm.To(OutputPrinter);
            //dpm.Extract();
            //dpm.PushToDevice();
            //dpm.ShowUsage();
            //dpm.SetDeviceOwner("com.fuck.aaa");
            return OK;
        }
    }
}
