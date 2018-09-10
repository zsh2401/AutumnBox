using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.ViewModel;
using AutumnBox.GUI.Windows;
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

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// UpdateNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateNoticeWindow : Window
    {
        internal UpdateNoticeWindow(UpdateCheckResult result)
        {
            InitializeComponent();
            DataContext = new VMUpdateWindow(result);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
