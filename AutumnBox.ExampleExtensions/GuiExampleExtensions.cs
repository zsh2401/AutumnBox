/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 17:52:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.ExampleExtensions
{
    [ExtRequiredDeviceStates(DeviceState.None)]
    [ExtName("ClearLove")]
    [ExtDesc("This is a fucker extension",Lang = "en-us")]
    [ExtDesc("这个模块将显示一个调试窗口")]
    [ExtIcon("meh.png")]
    public class GuiExampleExtensions : AutumnBoxExtension
    {
        public override int Main()
        {
            App.RunOnUIThread(() =>
            {
                App.ShowMessageBox("Fuck","Lover");
                App.CreateDebuggingWindow().Show();
            });
            return 0;
        }
    }
}
