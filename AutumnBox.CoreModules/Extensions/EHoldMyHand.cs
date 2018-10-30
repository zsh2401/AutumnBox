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
        bool stoppable = false;
        //public EHoldMyHand() {
        //    throw new System.Exception();
        //}
        protected override int VisualMain()
        {
            IMd5 md5 =  (IMd5)GetService("md5");
            WriteLine(md5.GetHashCode().ToString());
            md5 = (IMd5)GetService("md5");
            WriteLine(md5.GetHashCode().ToString());
            return OK;
            var input = Ux.InputString("Please be true");
            Ux.Message(input.ToString());
            return OK;
            WriteInitInfo();

            WriteLine("开始执行");
            Inputer inputer = GetDeviceCommander<Inputer>();
            inputer.PressKey(AndroidKeyCode.MediaNext);
            inputer.Tap(200, 300);
            WriteLine(inputer.GetHashCode().ToString());
            WriteLine(inputer.GetHashCode().ToString());
            Thread.Sleep(3000);
            WriteLine("进度25");
            Progress = 25;
            Thread.Sleep(3000);
            WriteLine("现在可以被停止了");
            Progress = 50;
            stoppable = true;
            Thread.Sleep(2500);
            Step(() =>
            {
                WriteLine("Haha");
            });
            Step(() =>
            {
                WriteLine("Hasdasdsaaha");
            });
            Thread.Sleep(5000);
            return 0;
        }
        protected override bool VisualStop()
        {
            return stoppable;
        }
    }
}
