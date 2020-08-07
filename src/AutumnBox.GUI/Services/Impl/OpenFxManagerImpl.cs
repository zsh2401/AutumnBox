using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IOpenFxManager))]
    class OpenFxManagerImpl : IOpenFxManager
    {
        public Task<object?>[] RunningTasks => OpenFx.Lake.Get<IExtensionTaskManager>().RunningTasks.ToArray();

        public IExtensionInfo[] Extensions => OpenFx.Lake.Get<ILibsManager>().Registry.Select((r) => r.ExtensionInfo).ToArray();

        private readonly ILogger logger = LoggerFactory.Auto(nameof(OpenFxManagerImpl));
        private readonly Queue<Action> handlers = new Queue<Action>();
        private bool isLoaded = false;
        public void LoadOpenFx()
        {
            logger.Info("Initializing Open Framework " + OpenFramework.BuildInfo.SDK_VERSION);
            OpenFx.Initialize(new AutumnBoxGuiBaseApiImpl());
            logger.Info("Open framework api system is initialized");
            var libsManager = OpenFx.Lake.Get<ILibsManager>();
            logger.Info($"There are {libsManager.Librarians.Count()} librarians and {libsManager.Registry.Count()} wrappers");
            libsManager.Registry.All((extInf) =>
            {
                logger.Info(extInf.ExtensionInfo.Id + " has been registerd by " + extInf.Librarian?.Name);
                return true;
            });
            isLoaded = true;
            while (handlers.Any())
            {
                try
                {
                    handlers.Dequeue()();
                }
                catch (Exception e)
                {
                    logger.Warn("", e);
                }
            }
        }

        public void RunExtension(string extensionClassName)
        {
            try
            {
                LakeProvider.Lake.Get<IExtensionTaskManager>().Start(extensionClassName);
            }
            catch (Exception e)
            {
                logger.Warn($"Can't start extension '{extensionClassName}'", e);
            }
        }

        public void WakeIfLoaded(Action callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (isLoaded) callback();
            else handlers.Enqueue(callback);
        }
    }
}
