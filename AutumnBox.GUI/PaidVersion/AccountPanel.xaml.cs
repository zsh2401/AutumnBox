using AutumnBox.GUI.UI.Fp;
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

namespace AutumnBox.GUI.PaidVersion
{
    /// <summary>
    /// AccountPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AccountPanel : FastPanelChild
    {
        public AccountPanel()
        {
            InitializeComponent();
            var fmt = App.Current.Resources["AccountInfoFmt"].ToString();
            IAccount acc = App.Current.AccountManager.Current;
            TBAccountInfo.Text = String.Format(fmt,acc.Id, acc.NickName, acc.RegisterDate,acc.ExpiredDate);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(App.Current.Resources["urlDvWebsite"].ToString());
        }
    }
}
