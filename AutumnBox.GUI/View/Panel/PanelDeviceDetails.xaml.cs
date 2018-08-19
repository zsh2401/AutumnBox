using AutumnBox.GUI.ViewModel;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelDeviceDetails.xaml 的交互逻辑
    /// </summary>
    public partial class PanelDeviceDetails : UserControl
    {
        public PanelDeviceDetails()
        {
            InitializeComponent();
            DataContext = new VMDeviceDetails();
        }
    }
}
