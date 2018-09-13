using AutumnBox.Basic.Device;
using AutumnBox.GUI.ViewModel;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelExtension.xaml 的交互逻辑
    /// </summary>
    public partial class PanelExtension : UserControl
    {
        public DeviceState TargetDeviceState { get; set; } = (DeviceState)255;
        public PanelExtension()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new VMExtensions(TargetDeviceState);
        }
    }
}
