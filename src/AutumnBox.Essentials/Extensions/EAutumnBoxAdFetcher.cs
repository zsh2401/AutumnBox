using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxAdFetcher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IAppManager appManager, IXCardsManager xCardsManager)
        {
            try
            {
                SLogger<EAutumnBoxAdFetcher>.Info($"XCARDMANAAGE:{xCardsManager}");
                appManager.RunOnUIThread(() =>
                {
                    xCardsManager.Register(new AdCard());
                });
            }
            catch (Exception e)
            {
                SLogger<EAutumnBoxAdFetcher>.Exception(e);
            }

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
