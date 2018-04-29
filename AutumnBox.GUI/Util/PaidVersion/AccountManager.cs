/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/29 17:25:03 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.PaidVersion
{
    class AccountManager : IAccountManager
    {
        public IAccount Current { get; private set; }

        public void AutoLogin()
        {
            this.Current = new ZshAccount();
        }

        public void Login(string userName, string pwd)
        {
            var zshAccount = new ZshAccount();
            if (userName == zshAccount.UserName && pwd.ToMd5() == zshAccount.Md5Pwd)
            {
                this.Current = new ZshAccount();
            }
            else {
                throw new Exception("Account not right!");
            }
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void Register(string userName, string pwd)
        {
            throw new NotImplementedException();
        }
    }
}
