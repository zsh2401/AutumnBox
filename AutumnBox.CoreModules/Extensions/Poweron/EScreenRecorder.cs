/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.CoreModules.View;
using AutumnBox.OpenFramework.Extension;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("屏幕录制器", "en-us:Screen recorder")]
    [ExtAuth("秋之盒官方", "en-us:AutumnBox official")]
    [ExtHide]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtVersion(1, 0, 0)]
    [ExtIcon("Icons.recorder.png")]
    [ExtOfficial(true)]
    internal class EScreenRecorder : AutumnBoxExtension
    {
        private ScreenRecorderWindow window;
        public override int Main(Dictionary<string, object> args)
        {
            App.RunOnUIThread(() =>
            {
                window = new ScreenRecorderWindow();
                window.Show();
            });
            return OK;
        }
        protected override bool OnStopCommand(object args)
        {
            App.RunOnUIThread(() =>
            {
                window.Close();
            });
            return true;
        }
    }
}
