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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// LoadingPanel.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingPanel : UserControl
    {
        public LoadingPanel()
        {
            InitializeComponent();
        }
        public void Close() {
            Visibility = Visibility.Collapsed;
        }
        public void SetProgress(int value) {
            PrgBar.Value = value;
        }
        public void SetMessage(string keyOrvalue) {
            TBTip.Text = UIHelper.GetString(keyOrvalue);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.DragMove();
        }
    }
}
