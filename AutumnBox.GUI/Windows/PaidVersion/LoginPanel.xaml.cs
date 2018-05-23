using AutumnBox.GUI.PaidVersion;
using AutumnBox.GUI.UI.Fp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows.PaidVersion
{
    /// <summary>
    /// InputAccountPanel.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPanel : FastPanelChild, ILoginUX
    {
        public override bool NeedShowBtnClose => false;
        private FastPanel ingPanel;
        public LoginPanel()
        {
            InitializeComponent();
            ingPanel = new FastPanel(GridBase, new LoginingPanel());
        }

        public void OnLoginFailed(Exception ex)
        {
            ingPanel.Hide();
        }

        public void OnLogining()
        {
            inputed = false;
            ingPanel.Display();
        }

        public void OnLoginSuccessed()
        {
            ingPanel.Hide();
            Finish();
        }
        bool inputed = false;
        public Tuple<string, string> WaitForInputFinished()
        {
            while (!inputed) ;
            Tuple<string, string> result = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                result = Tuple.Create(InputAccount.Text, InputPassword.Password);
            });
            return result;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            inputed = true;
        }
    }
}
