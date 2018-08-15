using AutumnBox.GUI.I18N;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
