/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/29 17:25:03 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Properties;
using AutumnBox.Support.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using AutumnBox.GUI.Util;
using System.Net;
using System.Text;
namespace AutumnBox.GUI.PaidVersion
{
    class AccountManager : IAccountManager
    {
        private string loginFmt;
        private string queryUserFmt;

        public void Init()
        {
            loginFmt = App.Current.Resources["urlApiLoginFmt"].ToString();
            queryUserFmt = App.Current.Resources["urlApiQueryuserFmt"].ToString();
        }

        private readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
        private readonly WebClient noCookieClient = new WebClient() { Encoding = Encoding.UTF8 };

        private string SavedToken
        {
            get
            {
                return Settings.Default.LoginKey;
            }
            set
            {
                Settings.Default.LoginKey = value;
                Settings.Default.Save();
            }
        }

        public IAccount Current { get; private set; }

        public ILoginUX UX { get; set; }

        public void AutoLogin()
        {
            Logger.Debug(this, "try auto logining");
            LoginUseSavedToken();
        }

        private string GetTokenFor(string uName, string uPwd)
        {
            Logger.Debug(this,loginFmt + uName + uPwd);
            var url = string.Format(loginFmt, uName, uPwd.ToMd5());
            var str = noCookieClient.DownloadString(url);
            var jObj = JObject.Parse(str);
            if (int.Parse(jObj["status_code"].ToString()) == 0)
            {
                return jObj["token"].ToString();
            }
            else
            {
                Logger.Debug(this, jObj);
                throw new Exception(jObj["message"]?.ToString());
            }
        }

        private Account GetAccountByToken(string token) {
            webClient.Headers["Cookie"] = $"token={token};";
            var url = string.Format(queryUserFmt);
            Logger.Debug(this, url);
            var responseText = webClient.DownloadString(url);
            Logger.Debug(this, responseText);
            if (int.Parse(JObject.Parse(responseText)["status_code"].ToString()) != 0)
            {
                throw new Exception("query user failed!");
            }
            else
            {
                return  JsonConvert.DeserializeObject<Account>(responseText);
            }
        }

        public void Logout()
        {
            Settings.Default.LoginKey = "";
            Settings.Default.Save();
            App.Current.Shutdown(1);
        }

        public void Login()
        {
            try
            {
                LoginUseSavedToken();
                CheckAccount(Current);
                App.Current.Dispatcher.Invoke(() =>
                    UX.OnLoginSuccessed());
                return;
            }
            catch (Exception ex)
            {
                Logger.Warn(this, "Login use saved token failed", ex);
            }
            Tuple<string, string> inputContent = null;
            string token = null;
            IAccount account = null;
            while (Current == null)
            {
                inputContent = UX.WaitForInputFinished();
                try
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        UX.OnLogining();
                    });
                    token = GetTokenFor(inputContent.Item1, inputContent.Item2);
                    account = GetAccountByToken(token);
                    CheckAccount(account);
                    SavedToken = token;
                    Current = account;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        UX.OnLoginSuccessed();
                    });
                }
                catch (Exception ex)
                {
                    ex.LogWarn(this, "Login failed");
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        UX.OnLoginFailed(ex);
                    });
                }
            }
        }

        private IAccount LoginUseSavedToken() {
            var acc = GetAccountByToken(SavedToken);
            CheckAccount(acc);
            return acc;
        }

        private void CheckAccount(IAccount account)
        {
            if (!account.IsVerified)
            {
                throw new AccountNotVerifiedException();
            }
            if (!account.IsPaid)
            {
                throw new AccountNotPaidException();
            }
        }
    }
}