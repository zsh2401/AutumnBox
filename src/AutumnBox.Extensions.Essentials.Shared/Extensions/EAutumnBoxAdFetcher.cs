using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxAdFetcher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IAppManager appManager, IXCardsManager xCardsManager)
        {
            return;
        }

        private class AdCard : IXCard
        {
            public int Priority => 0;

            public object View => "Ad";

            public void Create()
            {

            }

            public void Destory()
            {

            }

            public void Update()
            {
            }
        }
    }
}
