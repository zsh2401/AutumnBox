using AutumnBox.Essentials.ExternalXCards;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials
{
    public class EssentialsLibrarin : ExtensionLibrarian
    {
        [AutoInject]
        private IXCardsManager XCardManager { get; set; }

        [AutoInject]
        private IStorageManager StorageManager { get; set; }

        public const string ESSENTIALS_STORAGE_ID = "essentials_librarin_storage";

        public override string Name => "autumnbox-essentials";

        public override int MinApiLevel => 11;

        public override int TargetApiLevel => 11;

        public override void Ready()
        {
            base.Ready();
            SLogger<EssentialsLibrarin>.Info($"{nameof(EssentialsLibrarin)}'s ready");
            XCardManager.Register(new MotdXCard());
            XCardManager.Register(new AdXCard());
            StorageManager.Init(ESSENTIALS_STORAGE_ID);
            StorageManager.SaveJsonObject("ess", true);
        }
    }
}
