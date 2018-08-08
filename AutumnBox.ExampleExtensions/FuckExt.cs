/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 20:04:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System.Reflection;
using System.Threading;

namespace AutumnBox.ExampleExtensions
{
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.None)]
    [ExtName("重启测试器")]
    [ExtRunAsAdmin]
    public class FuckExt : AutumnBoxExtension
    {
        public override int Main()
        {
            var fx = new Factory().GetFx(this);
            App.RunOnUIThread(() =>
            {
                fx.ReloadLibs();
                App.RestartAppAsAdmin();
            });
            App.ShowLoadingWindow();
            Thread.Sleep(5000);
            App.CloseLoadingWindow();
            return 0;
        }
    }
}
