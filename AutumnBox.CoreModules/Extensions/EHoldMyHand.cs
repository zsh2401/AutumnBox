/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;

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
            StartExtension(typeof(EScreenShoter), (exitCode) =>
            {
                WriteLine(exitCode.ToString());
            }, new System.Collections.Generic.Dictionary<string, object>() {
                { KEY_CLOSE_FINISHED,true }
            });
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
