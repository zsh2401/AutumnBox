/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 20:04:55 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.ExampleExtensions
{
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.None)]
    class FuckExt : AutumnBoxExtension
    {
        public override int Main()
        {
            //Lib.Instance.RemoveOne();
            App.RunOnUIThread(() =>
            {
                App.RefreshExtensionList();
            });
            return 0;
        }
    }
}
