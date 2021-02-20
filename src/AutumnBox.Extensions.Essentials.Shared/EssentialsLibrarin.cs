using AutumnBox.Essentials.Extensions;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Open;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Essentials
{
    public class EssentialsLibrarin : AssemblyLibrarian
    {
        public static EssentialsLibrarin Current { get; private set; }

        [AutoInject]
        private IXCardsManager XCardManager { get; set; }

        [AutoInject]
        public IStorage StorageManager { get; private set; }

        [AutoInject]
        public ILake Lake { get; private set; }

        public const string ESSENTIALS_STORAGE_ID = "essentials_librarin_storage";

        public override string Name => "autumnbox-essentials";

        public override Version Version => new Version(1, 6, 0);

        public override int MinApiLevel => 11;

        public override int TargetApiLevel => 11;

        [AutoInject]
        private readonly INotificationManager notificationManager;

        [AutoInject]
        private readonly IExtensionTaskManager extensionTaskManager;

        [AutoInject(Id = "register")]
        private readonly IRegisterableLake rlake;

        [AutoInject]
        readonly IStorageManager storageManager;

        public IStorage Storage { get; private set; }

        public override void Ready()
        {
            base.Ready();
            Storage = storageManager.Open(nameof(EssentialsLibrarin));

            Current = this;
            SLogger<EssentialsLibrarin>.Info($"{nameof(EssentialsLibrarin)}'s ready");
            var last_update_check_time = Storage.ReadJsonObject<DateTime>("last_update_check_time");
            if (last_update_check_time == default || (last_update_check_time - DateTime.Now).TotalDays >= 1)
            {
                extensionTaskManager.Start(nameof(EAutumnBoxUpdateChecker));
                Storage.SaveJsonObject("last_update_check_time", DateTime.Now);
            }
            extensionTaskManager.Start(nameof(EAutumnBoxAdFetcher));
            extensionTaskManager.Start(nameof(EDonateCardRegister));

            var componentLoader = new ClassComponentsLoader("AutumnBox.Essentials", rlake, this.GetType().Assembly);
            componentLoader.Do();
        }

        public override void Destory()
        {
            base.Destory();
            Storage.ClearCache();
        }
    }
}
