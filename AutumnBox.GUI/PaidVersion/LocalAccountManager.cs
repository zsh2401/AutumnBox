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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.PaidVersion
{
    class LocalAccountManager : IAccountManager
    {
        private readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };

        public IAccount Current { get; private set; }

        private const string loginFmt = "http://localhost:4418/api/login?uname={0}&pwd={1}";
        private const string queryUserFmt = "http://localhost:4418/api/queryuser?key={0}";

        public void AutoLogin()
        {
            LoginUseSavedLoginKey();
        }

        private void LoginUseSavedLoginKey()
        {
            var url = string.Format(queryUserFmt, Settings.Default.LoginKey);
            var json = webClient.DownloadString(url);
            if (int.Parse(JObject.Parse(json)["status_code"].ToString()) != 0)
            {
                throw new Exception("query user failed!");
            }
            else
            {
                Current = JsonConvert.DeserializeObject<Account>(json);
                Logger.Debug(this, Current.ExpiredDate);
            }
        }

        public void Login(string userName, string pwd)
        {
            var url = string.Format(loginFmt, userName, pwd.ToMd5());
            var str = webClient.DownloadString(url);
            var jObj = JObject.Parse(str);
            Logger.Debug(this,jObj);
            if (int.Parse(jObj["status_code"].ToString()) == 0)
            {
                Settings.Default.LoginKey = jObj["key"].ToString();
                Settings.Default.Save();
                LoginUseSavedLoginKey();
            }
            else
            {
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
