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
        public DeviceState TargetDeviceState
        {
            set
            {
                DataContext = new VMExtensions(value);
            }
        }
        public PanelExtension()
        {
            InitializeComponent();
        }
    }
}
