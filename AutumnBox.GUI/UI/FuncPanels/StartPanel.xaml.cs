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

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlHelpOfLinkDevice"].ToString());
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlHelp"].ToString());
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlOfficialWebsite"].ToString());
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
           Process.Start(App.Current.Resources["urlOpensource"].ToString());
        }

        private void TextBlock_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlJoinQQGroup"].ToString());
        }

        private void TBDonateList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlDonateList"].ToString());
        }

        private void TBDonate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new FastPanel(App.Current.MainWindowAsMainWindow.GridMain, new DonatePanel()).Display() ;
        }
    }
}
