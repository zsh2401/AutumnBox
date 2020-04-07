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

        public override string Name => "autumnbox-essentials";

        public override int MinApiLevel => 11;

        public override int TargetApiLevel => 11;

        public override void Ready()
        {
            base.Ready();
            SLogger<EssentialsLibrarin>.Info(this.GetHashCode() + "::Ready");
            XCardManager.Register(new MotdXCard());
            XCardManager.Register(new AdXCard());
        }
    }
}
