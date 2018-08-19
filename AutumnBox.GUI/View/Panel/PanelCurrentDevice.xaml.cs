using System.Linq;
using System.Windows.Controls;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.ViewModel;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelCurrentDevice.xaml 的交互逻辑
    /// </summary>
    public partial class PanelCurrentDevice : UserControl
    {
        public PanelCurrentDevice()
        {
            InitializeComponent();
            DataContext = new VMCurrentDevice();
        }
    }
}
