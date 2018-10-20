/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:26:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using System.Windows;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("免ROOT修改DPI", "en-us:Modify dpi without root")]
    [ExtIcon("Icons.dpi.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EDpiChanger : AutumnBoxExtension
    {
        private Window mainWindow;
        protected override int Main()
        {
            Ux.Error("此模块尚未完成,请等待");
            return ERR;
        }
        protected override bool OnStopCommand(ExtensionStopArgs args)
        {
            App.RunOnUIThread(() =>
            {
                mainWindow?.Close();
            });
            return base.OnStopCommand(args);
        }
    }
}
