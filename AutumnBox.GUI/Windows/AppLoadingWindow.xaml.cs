using AutumnBox.GUI.Helper;
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
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// AppLoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppLoadingWindow : Window
    {
        public AppLoadingWindow()
        {
            InitializeComponent();
        }
        public void SetProgrress(double value) {
            PrgBar.Value = value;
        }
        public void SetTip(string keyOrValue) {
            TBTip.Text = UIHelper.GetString(keyOrValue);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
