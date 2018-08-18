using AutumnBox.GUI.ViewModel;
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

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelMain.xaml 的交互逻辑
    /// </summary>
    public partial class PanelMain : UserControl
    {
        public PanelMain()
        {
            InitializeComponent();
            DataContext = new VMPanelMain();
        }
    }
}
