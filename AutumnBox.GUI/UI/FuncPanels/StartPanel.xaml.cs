using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// StartPanel.xaml 的交互逻辑
    /// </summary>
    public partial class StartPanel : UserControl
    {
        public StartPanel()
        {
            InitializeComponent();
        }

        private void HyperLink_MouseClick(object sender, RoutedEventArgs e)
        {
            //new FastPanel(App.Current.MainWindowAsMainWindow.GridMain, new DonatePanel()).Display();
        }
    }
}
