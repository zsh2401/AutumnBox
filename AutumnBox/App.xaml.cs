using AutumnBox.Basic.Devices;
using System.Windows;

namespace AutumnBox
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static DeviceLink nowLink;
        internal static DevicesListener devicesListener = new DevicesListener();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
