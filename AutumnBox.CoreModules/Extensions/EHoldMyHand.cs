/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.OpenFramework.Extension;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    [ExtRequiredDeviceStates(NoMatter)]
    public class EHoldMyHand : OfficialVisualExtension
    {
        bool stoppable = false;
        protected override int VisualMain()
        {
            var brand = TargetDevice.GetProp(BuildPropKeys.Brand);
            WriteLine(TargetDevice.Product);
            Ux.ShowMessageDialog("Ok");
            WriteInitInfo();
            WriteLine("开始执行");
            Thread.Sleep(3000);
            WriteLine("进度25");
            Progress = 25;
            Thread.Sleep(3000);
            WriteLine("现在可以被停止了");
            Progress = 50;
            stoppable = true;
            Thread.Sleep(2500);
            return 0;                                                
        }
        protected override bool VisualStop()
        {
            return stoppable;
        }
    }
}
