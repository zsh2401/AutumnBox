using AutumnBox.GUI.Util.Net;
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
            HomeContentProvider.AfterLoaded((content) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    LoadingLine.Visibility = System.Windows.Visibility.Hidden;
                    if (content != null)
                    {
                        HomeContent.Content = content;
                    }
                });
            });
        }
    }
}
