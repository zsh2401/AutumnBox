namespace AutumnBox.GUI.PaidVersion
{
    interface IAccountManager
    {
        void Init();
        void Login(string userName, string pwd);
        void AutoLogin();
        IAccount Current { get; }
        void Logout();
    }
}
