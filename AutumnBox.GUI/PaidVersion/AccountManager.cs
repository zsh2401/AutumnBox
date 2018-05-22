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

        public IAccount Current { get; private set; }

        public void AutoLogin()
        {
            Logger.Debug(this,"try auto logining");
            LoginUseSavedLoginKey();
        }

        private void LoginUseSavedLoginKey()
        {
            webClient.Headers["Cookie"] = $"token={Settings.Default.LoginKey};";
            var url = string.Format(queryUserFmt);
            Logger.Debug(this, url);
            var responseText = webClient.DownloadString(url);
            Logger.Debug(this,responseText);
            if (int.Parse(JObject.Parse(responseText)["status_code"].ToString()) != 0)
            {
                throw new Exception("query user failed!");
            }
            else
            {
                Current = JsonConvert.DeserializeObject<Account>(responseText);
                Logger.Debug(this, Current.ExpiredDate);
            }
        }

        public void Login(string userName, string pwd)
        {
            var url = string.Format(loginFmt, userName, pwd.ToMd5());
            var str = noCookieClient.DownloadString(url);
            
            var jObj = JObject.Parse(str);
            if (int.Parse(jObj["status_code"].ToString()) == 0)
            {
                Settings.Default.LoginKey = jObj["token"].ToString();
                Settings.Default.Save();
                LoginUseSavedLoginKey();
            }
            else
            {
                Logger.Debug(this, jObj);
                throw new Exception(jObj["message"]?.ToString());
            }
        }

        public void Logout()
        {
            Settings.Default.LoginKey = "";
            Settings.Default.Save();
            App.Current.Shutdown(1);
        }
    }
}