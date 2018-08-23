/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("All testing")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.NoMatter)]
    [ExtUxEnable(true)]
    public class EHoldMyHand : AutumnBoxExtension
    {
        bool stoppable = false;
        public override int Main()
        {
            Ux.ShowLoadingWindow();
            //App.RunOnUIThread(() =>
            //{
            //    ChoiceResult choice = Ux.DoChoice("Do you like this?");
            //    Ux.ShowMessageDialog("OK",choice.ToString());
            //});
            Thread.Sleep(5000);
            Ux.CloseLoadingWindow();
            return 0;
        }
        public override bool OnStopCommand()
        {
            return stoppable;
        }
    }
}
