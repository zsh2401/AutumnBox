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
    [ExtRegions("en-US")]
    [ExtRequiredDeviceStates(NoMatter)]
    //[ExtDesc("wtf")]
    //[ObsoleteImageOperator]
    //[ExtAppProperty("com.miui.calculatorx")]
    [UserAgree("Please be true")]
    internal class EHoldMyHand : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            var thread = NewExtensionThread(typeof(EScreenShoter));
            thread.Data[KEY_CLOSE_FINISHED] = true;
            thread.Start();
            thread.WaitForExit();
            WriteLine(thread.ExitCode.ToString());
            return OK;
        }
    }
}
