using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.ViewModel;
using System.Windows;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// UpdateNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateNoticeWindow : Window
    {
        internal UpdateNoticeWindow(RemoteVersionInfoGetter.Result result)
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
