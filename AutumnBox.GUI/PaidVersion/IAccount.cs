using System;

namespace AutumnBox.GUI.PaidVersion
{
    interface IAccount
    {
        int Id { get; }
        string UserName { get; }
        bool IsActivate { get; }
        DateTime ExpiredDate { get; }
        DateTime RegisterDate { get; }
    }
}
