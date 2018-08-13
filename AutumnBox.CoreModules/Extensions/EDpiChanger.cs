/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:26:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("无ROOT修改DPI")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EDpiChanger : AutumnBoxExtension
    {
        public override int Main()
        {
            App.RunOnUIThread(() =>
            {
                new DpiChangeWindow(TargetDevice) { Owner = App.GetMainWindow() }.ShowDialog();
            });
            return OK;
        }
    }
}
