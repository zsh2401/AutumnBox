using AutumnBox.OpenFramework.Extension;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtIcon("Icons.download.png")]
    [ExtName("下载拓展模块","en-us:Download Extension")]
    [ExtPriority(int.MinValue)]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron | Basic.Device.DeviceState.Recovery | Basic.Device.DeviceState.Fastboot | AutumnBoxExtension.NoMatter)]
    class EDownloadExtension : SharpExtension
    {
        protected override void Processing(Dictionary<string, object> data)
        {
            Process.Start(App.GetPublicResouce<string>("urlDownloadExtensions"));
        }
    }
}
