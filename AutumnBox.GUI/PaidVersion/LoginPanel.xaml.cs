using AutumnBox.GUI.UI.Fp;
using AutumnBox.Support.Log;
using System;
using System.Windows;

namespace AutumnBox.GUI.PaidVersion
{
    /// <summary>
    /// InputAccountPanel.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPanel : FastPanelChild, ILoginUX
    {

        public LoginPanel()
        {
            InitializeComponent();
        }

        public override void OnPanelBtnCloseClicked(ref bool prevent)
        {
            base.OnPanelBtnCloseClicked(ref prevent);
            App.Current.Shutdown(1);
        }
        public void OnLoginFailed(Exception ex)
        {
            BtnLogin.Content = App.Current.Resources["btnLogin"];
            BtnLogin.IsEnabled = true;
            if (ex is AccountNotPaidException)
            {
                TBNotice.Text = App.Current.Resources["noticeAccountNotPaid"].ToString();
            }
            else if (ex is AccountNotVerifiedException)
            {
                TBNotice.Text = App.Current.Resources["noticeAccountNotVerified"].ToString();
            }
            else
            {
                TBNotice.Text = ex.Message;
            }
        }

        public void OnLogining()
        {
            inputed = false;
            BtnLogin.Content = App.Current.Resources["btnLogining"];
            BtnLogin.IsEnabled = false;
        }

        public void OnLoginSuccessed()
        {
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
            Logger.Debug(this, result.ToString());
            return result;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            inputed = true;
        }
    }
}
