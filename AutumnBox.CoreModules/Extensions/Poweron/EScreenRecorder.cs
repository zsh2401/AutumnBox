/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.CoreModules.View;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("屏幕录制器", "en-us:Screen recorder")]
    [ExtAuth("秋之盒官方", "en-us:AutumnBox official")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtVersion(1, 0, 0)]
    [ExtIcon("Icons.recorder.png")]
    [ExtOfficial(true)]
    internal class EScreenRecorder : AutumnBoxExtension
    {
        private ScreenRecorderWindow window;
        protected override int Main()
        {
            App.RunOnUIThread(() =>
            {
                window = new ScreenRecorderWindow();
                window.Show();
            });
            return OK;
        }
        protected override bool OnStopCommand(ExtensionStopArgs args)
        {
            App.RunOnUIThread(() =>
            {
                window.Close();
            });
            return true;
        }
    }
}
