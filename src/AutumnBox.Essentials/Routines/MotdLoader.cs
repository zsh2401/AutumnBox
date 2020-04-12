using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Routines
{
    internal sealed class MotdLoader : IXCard
    {
        [AutoInject]
        private readonly IXCardsManager xCardsManager;

        [AutoInject]
        private readonly IAppManager appManager;

        private const string MOTD_API_URL = "";

        public int Priority => 0;

        public object View { get; set; }


        public void Do()
        {
            View = "There should be MOTD!";
            appManager.RunOnUIThread(() =>
            {
                xCardsManager.Register(this);
            });
        }

        public void Create()
        {

        }

        public void Update()
        {

        }

        public void Destory()
        {

        }
    }
}
