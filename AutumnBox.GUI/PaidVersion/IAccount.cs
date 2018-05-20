using System;
using System.Net.Mail;

namespace AutumnBox.GUI.PaidVersion
{
    interface IAccount
    {
        int Id { get; }
        string NickName { get; }
        MailAddress EMail{get;}
        bool IsVerified { get; }
        DateTime ExpiredDate { get; }
        DateTime RegisterDate { get; }
        bool IsPaid { get;}
    }
}
