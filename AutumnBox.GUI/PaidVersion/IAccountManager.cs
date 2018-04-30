namespace AutumnBox.GUI.PaidVersion
{
    interface IAccountManager
    {
        void Login(string userName, string pwd);
        void AutoLogin();
        IAccount Current { get; }
        void Logout();
    }
}
