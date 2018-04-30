using AutumnBox.GUI.PaidVersion;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.Support.Log;
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

namespace AutumnBox.GUI.Windows.PaidVersion
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountManager am;
        internal LoginWindow(IAccountManager am)
        {
            InitializeComponent();
            this.am = am;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            TbInfo.Text = "登录中...";
            var inputUname = InputBoxUserName.Text;
            var inputPwd = InputBoxPwd.Password;
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        Logger.Debug(this, "Logining");
                        am.Login(inputUname, inputPwd);
                        Logger.Debug(this, "Logined");
                    }
                    catch(Exception ex)
                    {
                        Logger.DebugWarn(this,"Login failed",ex);
                        TbInfo.Text = "登录失败,请重试";
                    }
                });
            });
            if (am.Current != null)
            {
                DialogResult = true;
                Close();
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            //new FastPanel(GridBase).Display();
        }
    }
}
