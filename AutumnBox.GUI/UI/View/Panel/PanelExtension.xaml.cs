using AutumnBox.Basic.Device;
using AutumnBox.GUI.UI.ViewModel.Panel;
using AutumnBox.Support.Log;
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
