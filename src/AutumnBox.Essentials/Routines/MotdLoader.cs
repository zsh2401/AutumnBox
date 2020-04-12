using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Routines
{
    internal sealed class MotdLoader
    {
        [AutoInject]
        private IXCardsManager XCardsManager { get; set; }

        [AutoInject]
        private IAppManager AppManager { get; set; }

        private const string MOTD_API_URL = "";

        public void Do()
        {
           
        }
    }
}
