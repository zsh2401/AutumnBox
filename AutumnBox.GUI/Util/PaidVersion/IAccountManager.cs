using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.PaidVersion
{
    interface IAccountManager
    {
        void Register(string userName,string pwd);
        void Login(string userName, string pwd);
        void AutoLogin();
        IAccount Current { get; }
        void Logout();
    }
}
