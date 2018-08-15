using AutumnBox.GUI.UI.ViewModel.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.View.Panel
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
