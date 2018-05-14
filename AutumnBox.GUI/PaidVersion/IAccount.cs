using System;
namespace AutumnBox.GUI.PaidVersion
{
    interface IAccount
    {
        int Id { get; }
        string UserName { get; }
        bool IsActivated { get; }
        DateTime ExpiredDate { get; }
        DateTime RegisterDate { get; }
        bool IsPaid { get;}
    }
}
