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
        public IStorageManager StorageManager { get; private set; }

        [AutoInject]
        public ILake Lake { get; private set; }

        public const string ESSENTIALS_STORAGE_ID = "essentials_librarin_storage";

        public override string Name => "autumnbox-essentials";

        public override int MinApiLevel => 11;

        public override int TargetApiLevel => 11;

        [AutoInject]
        private readonly INotificationManager notificationManager;

        [AutoInject]
        private readonly IExtensionTaskManager extensionTaskManager;

        [AutoInject(Id = "register")]
        private readonly IRegisterableLake rlake;

        public override void Ready()
        {
            base.Ready();
            Current = this;
            SLogger<EssentialsLibrarin>.Info($"{nameof(EssentialsLibrarin)}'s ready");

            extensionTaskManager.Start(nameof(EAutumnBoxUpdateChecker));
            extensionTaskManager.Start(nameof(EAutumnBoxAdFetcher));
            extensionTaskManager.Start(nameof(EDonateCardRegister));

            var componentLoader = new ClassComponentsLoader("AutumnBox.Essentials", rlake, this.GetType().Assembly);
            componentLoader.Do();
        }

        private const string ROUTINE_DO_METHOD_NAME = "Do";
        private void RunRoutines<T>()
        {
            Task.Run(() =>
            {
                try
                {
                    var instance = new ObjectBuilder(typeof(T), Lake).Build();
                    var mProxy = new MethodProxy(instance, ROUTINE_DO_METHOD_NAME, Lake);
                    mProxy.Invoke();
                }
                catch (Exception e)
                {
                    SLogger<EssentialsLibrarin>.Warn("Routine is failed", e);
                }
            });
        }
    }
}
