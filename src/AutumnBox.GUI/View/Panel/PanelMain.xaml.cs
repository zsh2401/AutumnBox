using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.Util.Net.HomeContent;
using System.Threading.Tasks;
using System.Windows.Controls;

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
            Loaded += PanelMain_Loaded;
        }

        private void PanelMain_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HomeContentProvider.Refreshing += (s, _e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    LoadingLine.Visibility = System.Windows.Visibility.Visible;
                });
            };
            HomeContentProvider.Refreshed += (s, _e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    LoadingLine.Visibility = System.Windows.Visibility.Hidden;
                    HomeContent.Content = _e.NewContent;
                });
            };
            HomeContentProvider.Refreshing += (s, _e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    LoadingLine.Visibility = System.Windows.Visibility.Visible;
                });
            };
            Task.Run(() =>
            {
                HomeContentProvider.Do();
            });
        }
    }
}
