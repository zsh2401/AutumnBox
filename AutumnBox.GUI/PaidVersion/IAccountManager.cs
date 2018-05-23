namespace AutumnBox.GUI.PaidVersion
{
    interface IAccountManager
    {
        ILoginUX UX { get; set; }
        IAccount Current { get; }
        void Init();
        void Login();
        void Logout();
    }
}
