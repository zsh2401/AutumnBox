using AutumnBox.GUI.NetUtil;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// StartPanel.xaml 的交互逻辑
    /// </summary>
    public partial class StartPanel : UserControl, ILogSender
    {
        public StartPanel()
        {
            InitializeComponent();
        }

        public string LogTag => "WB";

        public bool IsShowLog => true;

        private void TBOfficialWebsite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Urls.OFFICIAL_WEBSITE);
        }

        private void WB_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Debug.WriteLine("heheh?" + e.Uri);
            if (!e.Uri.ToString().Contains("/api/start/"))
            {
                e.Cancel = true;
                Process.Start(e.Uri.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WB.Refresh();
        }
    }
}
