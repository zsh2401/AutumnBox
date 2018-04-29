/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/29 17:18:58 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.PaidVersion
{
    internal class ZshAccount : IAccount
    {
        public bool IsActivate => true;

        public string UserName =>"zsh2401";

        public string Md5Pwd => "6808412".ToMd5();

        public DateTime ExpiredDate => new DateTime(2019,5,15);

        public bool ChangePassword(string lastPwd,string newPwd)
        {
            throw new NotImplementedException();
        }

        public bool Recharge(string cdkey)
        {
            throw new NotImplementedException();
        }

        public void RefreshInfo()
        {
            throw new NotImplementedException();
        }
    }
}
