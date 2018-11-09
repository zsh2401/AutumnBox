/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    [ExtRequiredDeviceStates(NoMatter)]
    [ExtDesc("wtf")]
    //[ObsoleteImageOperator]6
    //[ExtAppProperty("com.miui.calculatorx")]
    [UserAgree("Please be true")]
    internal class EHoldMyHand : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            var recorder = GetDeviceCommander<VideoRecorder>();
            recorder.Seconds = 10;
            recorder.To(OutputPrinter);
            recorder.Start();
            return OK;
        }
    }
}
