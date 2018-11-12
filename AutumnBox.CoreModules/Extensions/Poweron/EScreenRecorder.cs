using AutumnBox.CoreModules.View;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
