using AutumnBox.Essentials.Routines;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using AutumnBox.OpenFramework.Open;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Essentials
{
    public class EssentialsLibrarin : ExtensionLibrarian
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

        public override void Ready()
        {
            base.Ready();
            Current = this;
            SLogger<EssentialsLibrarin>.Info($"{nameof(EssentialsLibrarin)}'s ready");
            RunRoutines<MotdLoader>();
            RunRoutines<AdLoader>();
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
