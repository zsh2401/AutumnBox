using System;

namespace AutumnBox.GUI.Util.PaidVersion
{
    interface IAccount
    {
        bool IsActivate { get; }
        string UserName { get; }
        string Md5Pwd { get; }
        void RefreshInfo();
        DateTime ExpiredDate { get; }
        bool Recharge(string cdkey);
        bool ChangePassword(string lastPwd,string newPwd);
    }
}
